using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetCompanyReviewsSummary
{
    public class GetCompanyReviewsSummaryQuery : IRequest<ReviewsSummaryDto>
    {
        public Guid CompanyId { get; }

        public GetCompanyReviewsSummaryQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
