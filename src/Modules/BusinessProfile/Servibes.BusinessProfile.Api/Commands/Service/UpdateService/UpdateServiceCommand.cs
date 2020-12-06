using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Service.UpdateService
{
    public class UpdateServiceCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
        public ServiceDto ServiceDto { get; set; }
    }
}
