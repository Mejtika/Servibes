using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.TimeReservations
{
    public class TimeReservationCreatedEvent : INotification
    {
        public Guid TimeReservationId { get; }

        public TimeReservationCreatedEvent(Guid timeReservationId)
        {
            TimeReservationId = timeReservationId;
        }
    }
}
