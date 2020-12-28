using System;
using MediatR;

namespace Servibes.Appointments.Application.Appointments.CancelClientAppointment
{
    public class CancelClientAppointmentCommand : IRequest
    {
        public Guid AppointmentId { get; }

        public string CancellationReason { get; }

        public CancelClientAppointmentCommand(Guid appointmentId, string cancellationReason)
        {
            AppointmentId = appointmentId;
            CancellationReason = cancellationReason;
        }
    }
}