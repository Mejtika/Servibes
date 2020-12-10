using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments
{
    public class Employee : ValueObject
    {
        public Guid EmployeeId { get; }

        public string Name { get; }

        private Employee(Guid employeeId, string name)
        {
            EmployeeId = employeeId;
            Name = name;
        }

        public static Employee Create(Guid employeeId, string name)
        {
            return new Employee(employeeId, name);
        }
    }
}