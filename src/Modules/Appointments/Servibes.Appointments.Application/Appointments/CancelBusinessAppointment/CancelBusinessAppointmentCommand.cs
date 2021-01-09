using System;
using MediatR;

namespace Servibes.Appointments.Application.Appointments.CancelBusinessAppointment
{
    public class CancelBusinessAppointmentCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid AppointmentId { get; }

        public string CancellationReason { get; }

        public CancelBusinessAppointmentCommand(Guid companyId, Guid appointmentId, string cancellationReason)
        {
            CompanyId = companyId;
            AppointmentId = appointmentId;
            CancellationReason = cancellationReason;
        }
    }
}