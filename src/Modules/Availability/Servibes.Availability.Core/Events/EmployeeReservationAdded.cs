using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Events
{
    public class EmployeeReservationAdded : IDomainEvent
    {
        public Employee Employee { get; }
        public Reservation Reservation { get; }

        public EmployeeReservationAdded(Employee employee, Reservation reservation)
        {
            Employee = employee;
            Reservation = reservation;
            throw new NotImplementedException();
        }
    }
}