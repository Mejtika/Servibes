using MediatR;
using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQuery : IRequest<IEnumerable<CompanyServicesDto>>
    {
        public GetCompanyServicesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
    }
}
