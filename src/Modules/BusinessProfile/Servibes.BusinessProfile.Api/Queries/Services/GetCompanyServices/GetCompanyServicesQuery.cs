using MediatR;
using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQuery : IRequest<IEnumerable<CompanyServicesDto>>
    {
        public Guid CompanyId { get; set; }
    }
}
