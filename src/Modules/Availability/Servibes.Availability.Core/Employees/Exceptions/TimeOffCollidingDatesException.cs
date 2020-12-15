using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Core.Employees.Exceptions
{
    public class TimeOffCollidingDatesException : DomainException
    {
        public TimeOffCollidingDatesException(TimeOff timeOff)
            :base($"Time off dates {timeOff.Start} and {timeOff.End} are colliding with existing one.")
        {
        }
    }
}