using System;
using MediatR;

namespace Servibes.Sales.Api.Events.External.AppointmentFinished
{
    public class AppointmentFinishedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid ReserveeId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public string ServiceName { get; }

        public decimal ServicePrice { get; }

        public AppointmentFinishedEvent(
            Guid appointmentId,
            Guid reserveeId,
            Guid companyId,
            Guid employeeId,
            DateTime start,
            DateTime end,
            string serviceName,
            decimal servicePrice)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            End = end;
            ServiceName = serviceName;
            ServicePrice = servicePrice;
        }
    }
}