using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeReservationAddedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }
        public Reservation Reservation { get; }

        public EmployeeReservationAddedDomainEvent(Employee employee, Reservation reservation)
        {
            Employee = employee;
            Reservation = reservation;
        }
    }
}