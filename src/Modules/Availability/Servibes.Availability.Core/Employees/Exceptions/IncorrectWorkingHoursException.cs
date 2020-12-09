using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Core.Employees.Exceptions
{
    public class IncorrectWorkingHoursException : DomainException
    {
        public IncorrectWorkingHoursException(string message)
            : base(message)
        {
        }
    }
}