using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.UpdateService
{
    public class UpdateServiceCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid ServiceId { get; }

        public ServiceDto ServiceDto { get; }

        public UpdateServiceCommand(Guid companyId, Guid serviceId, ServiceDto serviceDto)
        {
            CompanyId = companyId;
            ServiceId = serviceId;
            ServiceDto = serviceDto;
        }
    }
}
