using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetCompanyReviewsSummary
{
    public class GetCompanyReviewsSummaryQueryHandler : IRequestHandler<GetCompanyReviewsSummaryQuery, ReviewsSummaryDto>
    {
        private readonly BusinessProfileContext _context;

        public GetCompanyReviewsSummaryQueryHandler(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task<ReviewsSummaryDto> Handle(GetCompanyReviewsSummaryQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews.Where(x => x.CompanyId == request.CompanyId).ToListAsync(cancellationToken);
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
                    distributedReviews.Add(new ReviewSummaryDto { Rating = i, Count = 0, PercentOfTotal = 0 });
                }
            }

            var reviewSummary = new ReviewsSummaryDto
            {
                Reviews = distributedReviews.OrderBy(x => x.Rating).ToList(),
                Count = reviews.Count,
                Average = reviews.Select(x => x.StarsCount).DefaultIfEmpty().Average()
            };

            return reviewSummary;
        }
    }
}