using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.Appointments.Exceptions
{
    public class CannotChangeAppointmentStateException : DomainException
    {
        public CannotChangeAppointmentStateException(Guid appointmentId, AppointmentStatus current, AppointmentStatus next)
            :base($"Cannot change state for appointment {appointmentId} from {current} to {next}")
        {
        }
    }
}