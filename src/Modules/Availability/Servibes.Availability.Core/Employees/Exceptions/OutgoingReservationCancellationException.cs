using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Core.Employees.Exceptions
{
    public class OutgoingReservationCancellationException : DomainException
    {
        public OutgoingReservationCancellationException()
            : base($"Employee has an ongoing reservation, that cannot be cancelled.")
        {
        }
    }
}
