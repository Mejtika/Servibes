using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.CreateService
{
    public class CreateServiceCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public ServiceDto ServicDto { get; set; }
    }
}
