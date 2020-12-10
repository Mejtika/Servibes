using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeTimeOffReleasedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }
        public TimeOff TimeOff { get; }

        public EmployeeTimeOffReleasedDomainEvent(Employee employee, TimeOff timeOff)
        {
            Employee = employee;
            TimeOff = timeOff;
        }
    }
}