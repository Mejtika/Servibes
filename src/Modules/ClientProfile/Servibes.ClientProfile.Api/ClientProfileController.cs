using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servibes.ClientProfile.Api.Models;
using Servibes.ClientProfile.Api.Requests;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Exceptions;

namespace Servibes.ClientProfile.Api
{
    [ApiController]
    [Route("api")]
    public class ClientProfileController : ControllerBase
    {
        private readonly ClientProfileContext _context;
        private readonly IMessageBroker _messageBroker;

        public ClientProfileController(
            ClientProfileContext context,
            IMessageBroker messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
        }

        [HttpPost("account/reviews")]
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
            review.Status = ReviewStatus.Leaved;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("account/reviews")]
        public async Task<IActionResult> GetAllUserReviews()
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var client = await _context.Clients.Include(x => x.Reviews).SingleOrDefaultAsync(x => x.ClientId == userId);
            return Ok(client.Reviews);
        }

        [HttpGet("account/reviews/{reviewId}")]
        public async Task<IActionResult> GetUserReview(Guid reviewId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var client = await _context.Clients.Include(x => x.Reviews).SingleOrDefaultAsync(x => x.ClientId == userId);
            var review = client.Reviews.SingleOrDefault(x => x.ReviewId == reviewId);
            if (review == null)
            {
                throw new AppException($"Review with id {reviewId} not found.");
            }

            return Ok(review);
        }

        [HttpPost("account/favorites")]
        public async Task<IActionResult> AddToFavorites(AddToFavoritesRequest request)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var client = await _context.Clients.Include(x => x.Favorites).SingleOrDefaultAsync(x => x.ClientId == userId);
            client.Favorites.Add(new Favorite { CompanyId = request.CompanyId });
            return Ok();
        }

        [HttpGet("companies/{companyId}/reviews")]
        public async Task<IActionResult> GetAllCompanyReviews(Guid companyId)
        {
            var companyReviews = await _context.Reviews
                .Where(x => x.CompanyId == companyId).ToListAsync();
            var leavedReviews = companyReviews.Where(x => x.Status == ReviewStatus.Leaved);
            return Ok(leavedReviews);
        }
    }
}
