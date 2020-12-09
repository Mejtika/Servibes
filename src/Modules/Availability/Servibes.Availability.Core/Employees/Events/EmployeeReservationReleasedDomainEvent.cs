using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeReservationReleasedDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }
        public Reservation Reservation { get; }

        public EmployeeReservationReleasedDomainEvent(Employee employee, Reservation reservation)
        {
            Employee = employee;
            Reservation = reservation;
        }
    }
}