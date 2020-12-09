using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.TimeReservations.Exceptions
{
    public class TimeReservationIsAlreadyCanceledException : DomainException
    {
        public TimeReservationIsAlreadyCanceledException(Guid timeReservationId)
            :base($"Cannot cancel already canceled time reservation {timeReservationId}.")
        {
        }
    }
}