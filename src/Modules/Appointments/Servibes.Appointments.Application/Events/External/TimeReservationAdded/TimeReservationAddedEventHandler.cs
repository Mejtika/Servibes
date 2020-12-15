using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Application.Events.External.TimeReservationAdded
{
    public class TimeReservationAddedEventHandler : INotificationHandler<TimeReservationAddedEvent>
    {
        private readonly ITimeReservationRepository _timeReservationRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IDateTimeServer _dateTime;

        public TimeReservationAddedEventHandler(
            ITimeReservationRepository timeReservationRepository,
            IAppointmentUnitOfWork unitOfWork,
            IDateTimeServer dateTime)
        {
            _timeReservationRepository = timeReservationRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task Handle(TimeReservationAddedEvent notification, CancellationToken cancellationToken)
        {
            var reservedDate = ReservationDate.Create(notification.Start, notification.End, _dateTime.Now);
            var timeReservation = TimeReservation.Create(
                Guid.NewGuid(),
                notification.CompanyId,
                notification.EmployeeId,
                reservedDate);

            await _timeReservationRepository.AddAsync(timeReservation);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
