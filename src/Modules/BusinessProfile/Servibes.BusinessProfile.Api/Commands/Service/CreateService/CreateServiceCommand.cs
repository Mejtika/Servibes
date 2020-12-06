using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Service.CreateService
{
    public class CreateServiceCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public ServiceDto ServicDto { get; set; }
    }
}
