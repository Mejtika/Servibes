using System;
using MediatR;

namespace Servibes.Appointments.Application.Appointments.CancelBusinessAppointment
{
    public class CancelBusinessAppointmentCommand : IRequest
    {
        public Guid AppointmentId { get; }

        public string CancellationReason { get; }

        public CancelBusinessAppointmentCommand(Guid appointmentId, string cancellationReason)
        {
            AppointmentId = appointmentId;
            CancellationReason = cancellationReason;
        }
    }
}