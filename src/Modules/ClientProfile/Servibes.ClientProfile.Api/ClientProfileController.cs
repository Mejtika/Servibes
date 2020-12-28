using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servibes.ClientProfile.Api.Dtos;
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
            var reviews = await _context.Reviews.Where(x => x.ClientId == userId).ToListAsync();
            return Ok(reviews);
        }

        [HttpGet("account/reviews/{reviewId}")]
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

        [HttpPost("account/favorites")]
        public async Task<IActionResult> AddToFavorites(AddToFavoritesRequest request)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var favorite = new Favorite { CompanyId = request.CompanyId, ClientId = userId };
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("account/favorites/{companyId}")]
        public async Task<IActionResult> DeleteFromFavorites(Guid companyId)
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var favorite = await _context.Favorites.SingleOrDefaultAsync(x => x.CompanyId == companyId && x.ClientId == userId);
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("account/favorites")]
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

        [HttpGet("companies/{companyId}/reviews")]
        public async Task<IActionResult> GetAllCompanyReviews(Guid companyId)
        {
            var reviews = await _context.Reviews
                .Where(x => x.CompanyId == companyId).ToListAsync();
            var leavedReviews = reviews.Where(x => x.Status == ReviewStatus.Leaved).ToList();
            var companyReviews = new List<CompanyReviewDto>();
            foreach (var review in leavedReviews)
            {
                var client = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == review.ClientId);
                companyReviews.Add(new CompanyReviewDto
                {
                    Description = review.Description,
                    StarsCount = review.StarsCount,
                    Name = client.Name,
                });
            }
            return Ok(companyReviews);
        }

        [HttpGet("companies/{companyId}/reviews/summary")]
        public async Task<IActionResult> GetCompanyReviewsSummary(Guid companyId)
        {
            var reviews = (await _context.Reviews.Where(x => x.CompanyId == companyId).ToListAsync())
                .Where(x => x.Status == ReviewStatus.Leaved).ToList();
            var distributedReviews = reviews
            .GroupBy(x => x.StarsCount)
            .Select(x => new ReviewSummaryDto
            {
                Rating = x.Key,
                Count = x.Count(),
                PercentOfTotal = x.Count() * 100 / reviews.Count
            }).ToList();

            for (int i = 1; i <= 5; i++)
            {
                if (!distributedReviews.Exists(x => x.Rating == i))
                {
                    distributedReviews.Add(new ReviewSummaryDto {Rating = i, Count = 0, PercentOfTotal = 0});
                }
            }

            var reviewSummary = new ReviewsSummaryDto
            {
                Reviews = distributedReviews.OrderBy(x => x.Rating).ToList(),
                Count = reviews.Count,
                Average = reviews.Select(x => x.StarsCount).Average()
            };
            
            return Ok(reviewSummary);
        }
    }
}
