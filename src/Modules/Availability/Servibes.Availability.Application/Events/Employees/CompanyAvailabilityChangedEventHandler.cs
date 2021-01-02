using MediatR;
using Servibes.Availability.Application.Events.Companies;
using Servibes.Availability.Core.Companies.Events;
using Servibes.Availability.Core.Employees;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.Availability.Application.Events.Employees
{
    public class CompanyAvailabilityChangedEventHandler : INotificationHandler<CompanyAvailabilityChangedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public CompanyAvailabilityChangedEventHandler(
            IEmployeeRepository employeeRepository,
            IAvailabilityUnitOfWork unitOfWork)
        {
            this._employeeRepository = employeeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Task Handle(CompanyAvailabilityChangedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
