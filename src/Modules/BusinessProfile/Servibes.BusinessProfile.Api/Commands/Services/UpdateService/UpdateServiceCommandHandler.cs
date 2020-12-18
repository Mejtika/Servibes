using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Services.UpdateService
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly BusinessProfileContext _context;

        public UpdateServiceCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = _context.Services.SingleOrDefault(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId);

            if (service == null)
            {
                throw new AppException($"Service {request.ServiceId} or company {request.CompanyId} not found.");
            }

            var companyEmployees = await _context.Employees.Where(e => e.CompanyId == service.CompanyId).ToListAsync(cancellationToken);

            if (!companyEmployees.Any())
            {
                throw new AppException($"Company with id {service.CompanyId} doesn't have any employees.");
            }

            service.ServiceName = request.ServiceDto.ServiceName;
            service.Price = request.ServiceDto.Price;
            service.Duration = request.ServiceDto.Duration;
            service.Description = request.ServiceDto.Description;

            service.Performers.Clear();
            request.ServiceDto.Performers.Where(s => s.IsActive).ToList().ForEach(e =>
            {
                service.Performers.Add(new Performer()
                {
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
