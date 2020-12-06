using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Service.DeleteService
{
    public class DeleteServiceCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
