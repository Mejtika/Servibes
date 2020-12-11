using System;
using MediatR;

namespace Servibes.Appointments.Application.Events
{
    public class AppointmentCanceledEvent : INotification
    {
        public Guid AppointmentId { get; }

        public AppointmentCanceledEvent(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}