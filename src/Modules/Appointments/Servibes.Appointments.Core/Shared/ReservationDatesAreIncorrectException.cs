using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.Shared
{
    public class ReservationDatesAreIncorrectException : DomainException
    {
        public ReservationDatesAreIncorrectException(DateTime start, DateTime end)
            :base($"Reservation date {start} - {end} are not correct.")
        {
        }
    }
}