using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentConfirmedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public AppointmentConfirmedEvent(Guid appointmentId, Guid employeeId, Guid companyId)
        {
            AppointmentId = appointmentId;
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}