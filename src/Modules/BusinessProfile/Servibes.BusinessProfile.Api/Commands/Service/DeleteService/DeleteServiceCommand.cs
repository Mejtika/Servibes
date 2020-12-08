using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Commands.Service.DeleteService
{
    public class DeleteServiceCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
