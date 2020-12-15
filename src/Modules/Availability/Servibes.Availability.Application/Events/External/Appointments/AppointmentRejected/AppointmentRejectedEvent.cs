using System;
using MediatR;

namespace Servibes.Availability.Application.Events.External.Appointments.AppointmentRejected
{
    public class AppointmentRejectedEvent : INotification
    {
        public Guid CompanyId { get; }
        public Guid EmployeeId { get; }
        public DateTime Start { get; }

        public AppointmentRejectedEvent(Guid companyId, Guid employeeId, in DateTime start)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
        }
    }
}
