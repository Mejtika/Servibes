using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeAvailabilityDeletedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }

        public EmployeeAvailabilityDeletedDomainEvent(Employee employee)
        {
            Employee = employee;
        }
    }
}