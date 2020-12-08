using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<CompanyServicesDto>
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
