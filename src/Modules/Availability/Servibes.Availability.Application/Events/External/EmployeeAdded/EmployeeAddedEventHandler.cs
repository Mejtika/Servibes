using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Application.Events.External.EmployeeAdded
{
    public class EmployeeAddedEventHandler : INotificationHandler<EmployeeAddedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public EmployeeAddedEventHandler(
            IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository,
            IAvailabilityUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EmployeeAddedEvent notification, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(notification.CompanyId);
            if (company == null)
            {
                throw new Exception();
            }

            var openingHours = company.GetOpeningHours();
            var employee = Employee.Create(notification.EmployeeId, notification.CompanyId, openingHours);

            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}