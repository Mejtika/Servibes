using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.External.ReservationCancelled
{
    public class ReservationCancelledEvent : INotification
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public ReservationCancelledEvent(
            Guid companyId,
            Guid employeeId,
            DateTime start,
            DateTime end)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
    }
}
