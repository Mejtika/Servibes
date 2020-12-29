using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllCompanyReviews
{
    public class GetAllCompanyReviewsQueryHandler : IRequestHandler<GetAllCompanyReviewsQuery, IEnumerable<CompanyReviewDto>>
    {
        private readonly BusinessProfileContext _context;

        public GetAllCompanyReviewsQueryHandler(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyReviewDto>> Handle(GetAllCompanyReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews.Where(x => x.CompanyId == request.CompanyId).ToListAsync(cancellationToken);
            var companyReviews = new List<CompanyReviewDto>();
            foreach (var review in reviews)
            {
                var client = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == review.ClientId, cancellationToken);
                companyReviews.Add(new CompanyReviewDto
                {
                    Description = review.Description,
                    StarsCount = review.StarsCount,
                    Name = client.Name
                });
            }

            return companyReviews;
        }
    }
}