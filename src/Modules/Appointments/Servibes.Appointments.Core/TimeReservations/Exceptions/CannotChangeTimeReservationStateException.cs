using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.TimeReservations.Exceptions
{
    public class CannotChangeTimeReservationStateException : DomainException
    {
        public CannotChangeTimeReservationStateException(Guid timeReservationId, TimeReservationStatus current, TimeReservationStatus next) 
            : base($"Cannot change state for time reservation {timeReservationId} from {current} to {next}")
        {
        }
    }
}