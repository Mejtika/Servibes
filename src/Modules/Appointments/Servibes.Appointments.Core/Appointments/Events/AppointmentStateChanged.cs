using System;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments.Events
{
    public class AppointmentStateChanged : IDomainEvent
    {
        public Guid AppointmentId { get; }

        public Guid ReserveeId { get; }

        public Guid CompanyId { get; }

        public Employee Employee { get; }

        public ReservationDate ReservedDate { get; }

        public AppointmentStatus Status { get; }

        public string CancellationReason { get; }

        public Service Service { get; }

        public AppointmentStateChanged(
            Guid appointmentId,
            Guid reserveeId,
            Guid companyId,
            Employee employee,
            ReservationDate reservedDate,
            AppointmentStatus status,
            string cancellationReason, 
            Service service)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId;
            CompanyId = companyId;
            Employee = employee;
            ReservedDate = reservedDate;
            Status = status;
            CancellationReason = cancellationReason;
            Service = service;
        }
    }
}