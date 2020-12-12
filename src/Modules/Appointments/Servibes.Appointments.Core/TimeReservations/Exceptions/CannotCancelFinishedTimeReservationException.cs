using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.TimeReservations.Exceptions
{
    public class CannotCancelFinishedTimeReservationException : DomainException
    {
        public CannotCancelFinishedTimeReservationException(Guid timeReservationId)
            :base($"Finished time reservation {timeReservationId} cannot be canceled.")
        {
        }
    }
}