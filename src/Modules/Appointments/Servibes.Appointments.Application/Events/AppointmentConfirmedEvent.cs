using System;
using MediatR;

namespace Servibes.Appointments.Application.Events
{
    public class AppointmentConfirmedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public AppointmentConfirmedEvent(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}