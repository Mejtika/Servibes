using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Service.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly BusinessProfileContext context;

        public DeleteServiceCommandHandler(BusinessProfileContext context)
        {
            this.context = context;
        }

        public Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = context.Services.Where(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId).FirstOrDefault();

            if (service == null)
                throw new ArgumentException($"Service with id {request.ServiceId} and company id {request.CompanyId} doesnt exist.");

            context.Services.Remove(service);
            context.SaveChanges();

            return Unit.Task;
        }
    }
}
