using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Application.Events.External.ReservationCancelled;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Application.Events.External
{
    public class ReservationCancelledEventHandler : INotificationHandler<ReservationCancelledEvent>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITimeReservationRepository _timeReservationRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IDateTimeServer _dateTimeServer;

        public ReservationCancelledEventHandler(
            IAppointmentRepository appointmentRepository,
            ITimeReservationRepository timeReservationRepository,
            IAppointmentUnitOfWork unitOfWork,
            IDateTimeServer dateTimeServer)
        {
            _appointmentRepository = appointmentRepository;
            _timeReservationRepository = timeReservationRepository;
            _unitOfWork = unitOfWork;
            _dateTimeServer = dateTimeServer;
        }

        public async Task Handle(ReservationCancelledEvent notification, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(notification.CompanyId, notification.EmployeeId, notification.Start);
            var timeReservation = await _timeReservationRepository.GetAsync(notification.CompanyId, notification.EmployeeId, notification.Start);

            if (appointment == null && timeReservation == null)
            {
                throw new AppException($"Reservation at {notification.Start} not found.");
            }

            appointment?.Cancel(_dateTimeServer.Now, "Employee doesn't work in this company anymore.");
            timeReservation?.Cancel(_dateTimeServer.Now);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}