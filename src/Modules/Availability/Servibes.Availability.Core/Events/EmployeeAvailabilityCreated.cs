using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Events
{
    public class EmployeeAvailabilityCreated : IDomainEvent
    {
        public Employee Employee { get; }

        public EmployeeAvailabilityCreated(Employee employee)
        {
            Employee = employee;
        }
    }
}