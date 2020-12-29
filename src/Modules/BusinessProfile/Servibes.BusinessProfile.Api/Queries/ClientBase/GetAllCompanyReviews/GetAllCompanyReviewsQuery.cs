using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllCompanyReviews
{
    public class GetAllCompanyReviewsQuery : IRequest<IEnumerable<CompanyReviewDto>>
    {
        public Guid CompanyId { get; }

        public GetAllCompanyReviewsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
