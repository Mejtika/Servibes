using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeReservationCanceledDomainEvent : IDomainEvent
    {
        public Employee Employee { get; }
        public Reservation Reservation { get; }

        public EmployeeReservationCanceledDomainEvent(Employee employee, Reservation reservation)
        {
            Employee = employee;
            Reservation = reservation;
        }
    }
}