using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeAvailabilityCreatedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }

        public EmployeeAvailabilityCreatedDomainEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}