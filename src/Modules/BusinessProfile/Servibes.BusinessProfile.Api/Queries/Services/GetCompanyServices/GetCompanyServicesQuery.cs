using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQuery : IRequest<IEnumerable<CompanyServicesDto>>
    {
        public Guid CompanyId { get; set; }
    }
}
