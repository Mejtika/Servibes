using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.External.TimeReservationAdded
{
    public class TimeReservationAddedEvent : INotification
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public TimeReservationAddedEvent(
            Guid employeeId,
            Guid companyId,
            DateTime start,
            DateTime end)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
            Start = start;
            End = end;
        }
    }
}
