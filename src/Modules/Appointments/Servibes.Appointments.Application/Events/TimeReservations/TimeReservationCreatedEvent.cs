using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.TimeReservations
{
    public class TimeReservationCreatedEvent : INotification
    {
        public Guid TimeReservationId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public TimeReservationCreatedEvent(
            Guid timeReservationId,
            Guid companyId,
            Guid employeeId)
        {
            TimeReservationId = timeReservationId;
            CompanyId = companyId;
            EmployeeId = employeeId;
        }
    }
}
