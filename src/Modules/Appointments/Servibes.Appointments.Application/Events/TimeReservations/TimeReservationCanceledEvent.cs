using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.TimeReservations
{
    public class TimeReservationCanceledEvent : INotification
    {
        public Guid TimeReservationId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public TimeReservationCanceledEvent(Guid timeReservationId, Guid companyId, Guid employeeId, DateTime start)
        {
            TimeReservationId = timeReservationId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
        }
    }
}
