using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Service.UpdateService
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly BusinessProfileContext _context;

        public UpdateServiceCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Unit> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId);

            if (service == null)
                throw new ArgumentException($"Service with id {request.ServiceId} and company id {request.CompanyId} doesnt exist.");

            var companyEmployees = _context.Employees.Where(e => e.CompanyId == service.CompanyId).ToList();

            if (companyEmployees.Count == 0)
                throw new ArgumentException($"Company with id {service.CompanyId} doesnt have any employees.");

            service.ServiceName = request.ServiceDto.ServiceName;
            service.Price = request.ServiceDto.Price;
            service.Duration = request.ServiceDto.Duration;
            service.Description = request.ServiceDto.Description;

            service.Performers.Clear();
            request.ServiceDto.Performers.Where(s => s.IsActive).ToList().ForEach(e =>
            {
                service.Performers.Add(new Model.Performer()
                {
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });

            _context.Services.Update(service);
            _context.SaveChanges();

            return Unit.Task;
        }
    }
}
