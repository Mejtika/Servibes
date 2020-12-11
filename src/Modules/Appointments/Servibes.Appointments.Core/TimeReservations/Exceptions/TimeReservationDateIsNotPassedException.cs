using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.TimeReservations
{
    public class TimeReservationDateIsNotPassedException : DomainException
    {
        public TimeReservationDateIsNotPassedException(Guid timeReservationId)
            :base($"Time reservation {timeReservationId} date is not passed.")
        {
        }
    }
}