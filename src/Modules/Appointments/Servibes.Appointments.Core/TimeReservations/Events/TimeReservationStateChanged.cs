using System;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservations
{
    public class TimeReservationStateChanged : IDomainEvent
    {
        public Guid TimeReservationId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public ReservationDate Date { get; }

        public TimeReservationStatus Status { get; }

        public TimeReservationStateChanged(
            Guid timeReservationId,
            Guid companyId,
            Guid employeeId,
            ReservationDate date,
            TimeReservationStatus status)
        {
            TimeReservationId = timeReservationId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Date = date;
            Status = status;
        }
    }
}