using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Application.Events.External.TimeReservations.TimeReservationFinished
{
    public class TimeReservationFinishedEventHandler : INotificationHandler<TimeReservationFinishedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public TimeReservationFinishedEventHandler(IEmployeeRepository employeeRepository, IAvailabilityUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TimeReservationFinishedEvent notification, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(notification.EmployeeId);
            var reservation = employee.GetReservationByDate(notification.Start);
            employee.ReleaseReservation(reservation);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}