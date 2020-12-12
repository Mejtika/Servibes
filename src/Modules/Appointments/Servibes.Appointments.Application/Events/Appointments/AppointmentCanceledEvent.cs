using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentCanceledEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public AppointmentCanceledEvent(Guid appointmentId, Guid employeeId, Guid companyId)
        {
            AppointmentId = appointmentId;
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}