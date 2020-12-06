using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Service.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
    {
        private readonly BusinessProfileContext context;

        public CreateServiceCommandHandler(BusinessProfileContext context)
        {
            this.context = context;
        }

        public Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var companyEmployees = context.Employees.Where(e => e.CompanyId == request.CompanyId);

            if (companyEmployees.Count() == 0)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt have any employees.");

            Model.Service service = new Model.Service()
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
                service.Performers.Add(new Model.Performer()
                {
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });

            context.Services.Add(service);
            context.SaveChanges();

            return Task.FromResult(service.ServiceId);
        }
    }
}
