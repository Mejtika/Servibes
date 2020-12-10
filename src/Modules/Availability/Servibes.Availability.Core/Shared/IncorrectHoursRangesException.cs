using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Core.Shared
{
    public class IncorrectHoursRangesException : DomainException
    {
        public IncorrectHoursRangesException(string message)
            : base(message)
        {
        }
    }
}