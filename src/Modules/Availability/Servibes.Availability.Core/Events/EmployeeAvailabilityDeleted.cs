using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Events
{
    public class EmployeeAvailabilityDeleted : IDomainEvent
    {
        public Employee Employee { get; }

        public EmployeeAvailabilityDeleted(Employee employee)
        {
            Employee = employee;
            throw new NotImplementedException();
        }
    }
}