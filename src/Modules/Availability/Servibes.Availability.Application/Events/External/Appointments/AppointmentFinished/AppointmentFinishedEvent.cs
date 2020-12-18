using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.Appointments
{
    public class AppointmentFinishedEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid ReserveeId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public decimal Price { get; }

        public AppointmentFinishedEvent(
            Guid appointmentId,
            Guid reserveeId,
            Guid companyId,
            Guid employeeId,
            DateTime start, 
            DateTime end, 
            decimal price)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            End = end;
            Price = price;
        }
    }
}