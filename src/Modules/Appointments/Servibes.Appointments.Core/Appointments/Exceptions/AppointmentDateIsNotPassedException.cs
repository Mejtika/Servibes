using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.Appointments.Exceptions
{
    public class AppointmentDateIsNotPassedException : DomainException
    {
        public AppointmentDateIsNotPassedException(Guid appointmentId)
            :base($"Appointment {appointmentId} date is not passed.")
        {
        }
    }
}