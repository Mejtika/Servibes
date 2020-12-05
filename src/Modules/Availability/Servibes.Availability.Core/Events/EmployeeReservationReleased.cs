using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Events
{
    public class EmployeeReservationReleased : IDomainEvent
    {
        public Employee Employee { get; }
        public Reservation Reservation { get; }

        public EmployeeReservationReleased(Employee employee, Reservation reservation)
        {
            Employee = employee;
            Reservation = reservation;
        }
    }
}