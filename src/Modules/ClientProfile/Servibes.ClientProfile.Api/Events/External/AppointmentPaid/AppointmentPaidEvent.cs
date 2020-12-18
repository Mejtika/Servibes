using System;
using MediatR;

namespace Servibes.ClientProfile.Api.Events.External.AppointmentPaid
{
    public class AppointmentPaidEvent : INotification
    {
        public Guid AppointmentId { get; }

        public Guid ReserveeId { get; }

        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public decimal Price { get; }

        public AppointmentPaidEvent(
            Guid appointmentId,
            Guid reserveeId,
            Guid companyId,
            Guid employeeId,
            decimal price)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            Price = price;
        }
    }
}
