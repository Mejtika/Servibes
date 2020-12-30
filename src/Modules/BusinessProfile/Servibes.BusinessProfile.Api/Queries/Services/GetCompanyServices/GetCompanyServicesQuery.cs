using MediatR;
using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQuery : IRequest<IEnumerable<CompanyServiceDto>>
    {
        public Guid CompanyId { get; }

        public GetCompanyServicesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
