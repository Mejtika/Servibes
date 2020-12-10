using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.Appointments.Exceptions
{
    public class CannotCancelStartedAppointmentException : DomainException
    {
        public CannotCancelStartedAppointmentException(Guid appointmentId)
        : base($"Cannot cancel appointment {appointmentId}, it is already started.")
        {
        }
    }
}