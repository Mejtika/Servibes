using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<CompanyServicesDto>
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
