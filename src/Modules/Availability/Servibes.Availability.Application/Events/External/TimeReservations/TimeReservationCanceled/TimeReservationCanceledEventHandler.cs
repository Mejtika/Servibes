using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Application.Events.External.TimeReservations.TimeReservationCanceled
{
    public class TimeReservationCanceledEventHandler : INotificationHandler<TimeReservationCanceledEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public TimeReservationCanceledEventHandler(IEmployeeRepository employeeRepository, IAvailabilityUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TimeReservationCanceledEvent notification, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(notification.EmployeeId);
            var reservation = employee.GetReservationByDate(notification.Start);
            employee.ReleaseReservation(reservation);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}