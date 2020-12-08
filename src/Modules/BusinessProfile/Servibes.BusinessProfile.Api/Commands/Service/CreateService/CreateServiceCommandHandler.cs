﻿using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Service.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
    {
        private readonly BusinessProfileContext _context;

        public CreateServiceCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var companyEmployees = _context.Employees.Where(e => e.CompanyId == request.CompanyId);

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
                    //TODO: Add employee existance checking or add performers by EmployeeId directly
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });

            _context.Services.Add(service);
            _context.SaveChanges();

            return Task.FromResult(service.ServiceId);
        }
    }
}
