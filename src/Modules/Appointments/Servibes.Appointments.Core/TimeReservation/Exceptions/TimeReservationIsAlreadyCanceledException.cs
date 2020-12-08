using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.TimeReservation
{
    public class TimeReservationIsAlreadyCanceledException : DomainException
    {
        public TimeReservationIsAlreadyCanceledException(Guid timeReservationId)
            :base($"Cannot cancel already canceled time reservation {timeReservationId}.")
        {
        }
    }
}