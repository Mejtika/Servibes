using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments.Events
{
    public class AppointmentStateChanged : IDomainEvent
    {
        public Guid AppointmentId { get; }

        public AppointmentStatus Status { get; }

        public AppointmentStateChanged(Guid appointmentId, AppointmentStatus status)
        {
            AppointmentId = appointmentId;
            Status = status;
        }
    }
}