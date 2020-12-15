using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Application.Events.Appointments;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Application.Events.External.Appointments.AppointmentFinished
{
    public class AppointmentFinishedEventHandler : INotificationHandler<AppointmentFinishedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public AppointmentFinishedEventHandler(IEmployeeRepository employeeRepository, IAvailabilityUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AppointmentFinishedEvent notification, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(notification.EmployeeId);
            var reservation = employee.GetReservationByDate(notification.Start);
            employee.ReleaseReservation(reservation);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}