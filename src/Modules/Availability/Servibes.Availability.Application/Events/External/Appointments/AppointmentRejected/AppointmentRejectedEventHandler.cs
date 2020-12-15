using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Application.Events.External.Appointments.AppointmentRejected
{
    public class AppointmentRejectedEventHandler : INotificationHandler<AppointmentRejectedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public AppointmentRejectedEventHandler(
            IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository,
            IAvailabilityUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AppointmentRejectedEvent notification, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(notification.CompanyId);
            if (company == null)
            {
                throw new Exception($"Company {notification.CompanyId} doesn't exists.");
            }

            var employee = await _employeeRepository.GetByIdAsync(notification.EmployeeId);
            var reservation = employee.GetReservationByDate(notification.Start);
            employee.ReleaseReservation(reservation);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}