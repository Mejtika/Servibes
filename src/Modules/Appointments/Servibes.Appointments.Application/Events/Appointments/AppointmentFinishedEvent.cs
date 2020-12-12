using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentFinishedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public AppointmentFinishedEvent(Guid appointmentId, Guid employeeId, Guid companyId)
        {
            AppointmentId = appointmentId;
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}