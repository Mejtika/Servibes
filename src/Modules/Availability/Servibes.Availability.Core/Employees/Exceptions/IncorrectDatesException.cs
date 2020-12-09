using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Core.Employees.Exceptions
{
    public class IncorrectDatesException : DomainException
    {
        public IncorrectDatesException(string name, DateTime start, DateTime end)
            : base($"{name} dates {start} and {end} are incorrect.")
        {
        }
    }
}