using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Commands.Services.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly BusinessProfileContext _context;

        public DeleteServiceCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
                .SingleOrDefaultAsync(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId, cancellationToken);

            if (service == null)
                throw new ArgumentException($"Service {request.ServiceId} or company {request.CompanyId} not found.");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
