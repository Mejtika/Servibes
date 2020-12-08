using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Commands.Service.CreateService
{
    public class CreateServiceCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public ServiceDto ServicDto { get; set; }
    }
}
