using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Events.External.AppointmentCreated
{
    public class AppointmentCreatedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid ReserveeId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public AppointmentCreatedEvent(
            Guid appointmentId,
            Guid reserveeId,
            Guid companyId,
            Guid employeeId, 
            DateTime start,
            DateTime end)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
    }
}
