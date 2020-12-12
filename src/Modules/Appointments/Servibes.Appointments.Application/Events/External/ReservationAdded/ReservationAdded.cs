using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.External.ReservationAdded
{
    public class ReservationAddedEvent : INotification
    {
        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public ReservationAddedEvent(Guid employeeId, DateTime start)
        {
            EmployeeId = employeeId;
            Start = start;
        }
    }
}
