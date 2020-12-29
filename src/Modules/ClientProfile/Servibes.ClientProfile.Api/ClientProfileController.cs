using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servibes.ClientProfile.Api.Dtos;
using Servibes.ClientProfile.Api.Events;
using Servibes.ClientProfile.Api.Models;
using Servibes.ClientProfile.Api.Requests;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.ClientProfile.Api
{
    [ApiController]
    [Route("api/account")]
    public class ClientProfileController : ControllerBase
    {
        private readonly ClientProfileContext _context;
        private readonly IMessageBroker _messageBroker;
        private readonly IDateTimeServer _dateTimeServer;

        public ClientProfileController(
            ClientProfileContext context,
            IMessageBroker messageBroker,
            IDateTimeServer dateTimeServer)
        {
            _context = context;
            _messageBroker = messageBroker;
            _dateTimeServer = dateTimeServer;
        }

        [HttpPost("reviews")]
        public async Task<IActionResult> LeaveReview(LeaveReviewRequest request)
        {
            var review = _context.Reviews.SingleOrDefault(x => x.ReviewId == request.ReviewId);
            if (review == null)
            {
                throw new AppException($"Review with id {request.ReviewId} not found");
            }

            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            if (review.ClientId != userId)
            {
                throw new AppException($"Leaving review require authorized user.");
            }

            if (review.Status != ReviewStatus.New)
            {
                throw new AppException($"Review with id {request.ReviewId} already exists.");
            }

            review.Description = request.Description;
            review.StarsCount = request.StarsCount;
            review.Status = ReviewStatus.Added;
            review.AddedOn = _dateTimeServer.Now;

            await _context.SaveChangesAsync();
            var @event = new ReviewAddedEvent(
                review.ReviewId,
                review.ClientId,
                review.CompanyId,
                review.Description,
                review.StarsCount.Value,
                review.AddedOn.Value);

            await _messageBroker.PublishAsync(new[] {@event});
            return Ok();
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> GetAllUserReviews()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var reviews = await _context.Reviews.Where(x => x.ClientId == userId).ToListAsync();
            return Ok(reviews);
        }

        [HttpGet("reviews/{reviewId}")]
        public async Task<IActionResult> GetUserReview(Guid reviewId)
        {
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var review = await _context.Reviews.SingleOrDefaultAsync(x => x.ReviewId == reviewId && x.ClientId == userId);
            if (review == null)
            {
                throw new AppException($"Review with id {reviewId} not found.");
            }

            return Ok(review);
        }

        [HttpPost("favorites")]
        public async Task<IActionResult> AddToFavorites(AddToFavoritesRequest request)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var favorite = new Favorite { CompanyId = request.CompanyId, ClientId = userId };
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("favorites/{companyId}")]
        public async Task<IActionResult> DeleteFromFavorites(Guid companyId)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var favorite = await _context.Favorites.SingleOrDefaultAsync(x => x.CompanyId == companyId && x.ClientId == userId);
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetClientFavorites()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var favorites = await _context.Favorites.Where(x => x.ClientId == userId).ToListAsync();
            var favoritesCompaniesDto = favorites
                .Select(x => new FavoritesCompaniesDto
                {
                    CompanyId = x.CompanyId
                }).ToList();
            return Ok(favoritesCompaniesDto);
        }
    }
}
