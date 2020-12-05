using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Events
{
    public class EmployeeReservationCanceled : IDomainEvent
    {
        public Employee Employee { get; }
        public Reservation Reservation { get; }

        public EmployeeReservationCanceled(Employee employee, Reservation reservation)
        {
            Employee = employee;
            Reservation = reservation;
            throw new NotImplementedException();
        }
    }
}