using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.TimeReservations
{
    public class TimeReservationFinishedEvent : INotification
    {
        public Guid TimeReservationId { get; }

        public TimeReservationFinishedEvent(Guid timeReservationId)
        {
            TimeReservationId = timeReservationId;
        }
    }
}
