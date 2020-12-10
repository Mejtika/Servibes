using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeTimeOffReceivedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }
        public TimeOff Timeoff { get; }

        public EmployeeTimeOffReceivedDomainEvent(Employee employee, TimeOff timeoff)
        {
            Employee = employee;
            Timeoff = timeoff;
        }
    }
}