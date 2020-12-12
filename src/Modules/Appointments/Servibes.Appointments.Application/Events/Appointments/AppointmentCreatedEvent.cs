using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentCreatedEvent : INotification
    {
        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public AppointmentCreatedEvent(Guid employeeId, DateTime start, DateTime end )
        {
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
    }
}
