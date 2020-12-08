using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.UpdateService
{
    public class UpdateServiceCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
        public ServiceDto ServiceDto { get; set; }
    }
}
