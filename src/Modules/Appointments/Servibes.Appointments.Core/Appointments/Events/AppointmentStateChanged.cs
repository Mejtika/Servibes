using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments.Events
{
    public class AppointmentStateChanged : IDomainEvent
    {
        public Guid AppointmentId { get; }

        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public AppointmentStatus Status { get; }

        public AppointmentStateChanged(Guid appointmentId, Guid employeeId, Guid companyId, DateTime start, DateTime end, AppointmentStatus status)
        {
            AppointmentId = appointmentId;
            EmployeeId = employeeId;
            CompanyId = companyId;
            Start = start;
            End = end;
            Status = status;
        }
    }
}