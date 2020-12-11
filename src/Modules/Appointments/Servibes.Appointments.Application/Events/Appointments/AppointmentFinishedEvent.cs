using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentFinishedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public AppointmentFinishedEvent(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}