using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.CreateService
{
    public class CreateServiceCommand : IRequest<Guid>
    {
        public Guid CompanyId { get;  }

        public ServiceDto ServicDto { get;  }

        public CreateServiceCommand(Guid companyId, ServiceDto servicDto)
        {
            CompanyId = companyId;
            ServicDto = servicDto;
        }
    }
}
