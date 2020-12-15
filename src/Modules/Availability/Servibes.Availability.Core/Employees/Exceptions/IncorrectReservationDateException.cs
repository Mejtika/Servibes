using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Core.Employees.Exceptions
{
    public class IncorrectReservationDateException : DomainException
    {
        public IncorrectReservationDateException(Reservation reservation)
            : base($"Reservation for chosen date {reservation.Start} - {reservation.End} cannot be made.")
        {
        }
    }
}