using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentCreatedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public AppointmentCreatedEvent(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
