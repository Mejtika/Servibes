using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.TimeReservations
{
    public class TimeReservationCanceledEvent : INotification
    {
        public Guid TimeReservationId { get; }

        public TimeReservationCanceledEvent(Guid timeReservationId)
        {
            TimeReservationId = timeReservationId;
        }
    }
}
