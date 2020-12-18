using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Services.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
    {
        private readonly BusinessProfileContext _context;

        public CreateServiceCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var companyEmployees = await _context.Employees.Where(e => e.CompanyId == request.CompanyId).ToListAsync(cancellationToken);

            if (!companyEmployees.Any())
            {
                throw new AppException($"Company with id {request.CompanyId} doesn't have any employees.");
            }

            var service = new Service()
            {
                ServiceId = Guid.NewGuid(),
                CompanyId = request.CompanyId,
                ServiceName = request.ServicDto.ServiceName,
                Description = request.ServicDto.Description,
                Duration = request.ServicDto.Duration,
                Price = request.ServicDto.Price,
            };

            request.ServicDto.Performers.Where(s => s.IsActive).ToList().ForEach(e =>
            {
                service.Performers.Add(new Performer()
                {
                    //TODO: Add employee existance checking or add performers by EmployeeId directly
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });

            await _context.Services.AddAsync(service, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return service.ServiceId;
        }
    }
}
