using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.DeleteService
{
    public class DeleteServiceCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
