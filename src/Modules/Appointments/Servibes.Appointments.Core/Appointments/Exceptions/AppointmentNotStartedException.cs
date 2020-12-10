using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.Appointments.Exceptions
{
    public class AppointmentNotStartedException : DomainException
    {
        public AppointmentNotStartedException(Guid appointmentId)
        : base($"Appointment {appointmentId} is not started yet.")
        {
        }
    }
}