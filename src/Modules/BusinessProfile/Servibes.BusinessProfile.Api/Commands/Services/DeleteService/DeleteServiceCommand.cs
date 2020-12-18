using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.DeleteService
{
    public class DeleteServiceCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid ServiceId { get; }

        public DeleteServiceCommand(Guid companyId, Guid serviceId)
        {
            CompanyId = companyId;
            ServiceId = serviceId;
        }
    }
}
