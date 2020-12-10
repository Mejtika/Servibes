using System;
using Servibes.Shared.Exceptions;

namespace Servibes.Appointments.Core.Appointments.Exceptions
{
    public class CannotCancelFinishedAppointmentException : DomainException
    {
        public CannotCancelFinishedAppointmentException(Guid appointmentId)
            :base($"Finished appointment cannot be canceled.")
        {
        }
    }
}