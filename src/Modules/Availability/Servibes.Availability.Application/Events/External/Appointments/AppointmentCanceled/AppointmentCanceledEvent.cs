using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentCanceledEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid ReserveeId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public string CancellationReason { get; }

        public AppointmentCanceledEvent(
            Guid appointmentId,
            Guid reserveeId,
            Guid companyId,
            Guid employeeId,
            DateTime start,
            DateTime end,
            string cancellationReason)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            End = end;
            CancellationReason = cancellationReason;
        }
    }
}