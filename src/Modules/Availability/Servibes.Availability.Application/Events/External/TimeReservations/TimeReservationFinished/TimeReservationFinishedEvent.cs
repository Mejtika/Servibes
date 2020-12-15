using System;
using MediatR;

namespace Servibes.Availability.Application.Events.External.TimeReservations.TimeReservationFinished
{
    public class TimeReservationFinishedEvent : INotification
    {
        public Guid TimeReservationId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public TimeReservationFinishedEvent(Guid timeReservationId, Guid companyId, Guid employeeId, DateTime start)
        {
            TimeReservationId = timeReservationId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
        }
    }
}
