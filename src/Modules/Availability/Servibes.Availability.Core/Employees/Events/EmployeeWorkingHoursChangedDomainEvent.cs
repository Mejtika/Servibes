using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeWorkingHoursChangedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }

        public EmployeeWorkingHoursChangedDomainEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}