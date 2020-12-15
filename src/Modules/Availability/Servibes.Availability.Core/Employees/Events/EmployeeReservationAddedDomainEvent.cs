using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees.Events
{
    public class EmployeeReservationAddedDomainEvent : IDomainEvent
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public Reservation Reservation { get; }

        public ReservationSnapshot ReservationSnapshot { get; }

        public EmployeeReservationAddedDomainEvent(
            Guid employeeId,
            Guid companyId,
            Reservation reservation,
            ReservationSnapshot snapshot)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
            Reservation = reservation;
            ReservationSnapshot = snapshot;
        }
    }
}