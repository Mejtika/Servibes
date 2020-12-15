using System;
using MediatR;

namespace Servibes.Availability.Application.Employees.MakeReservation
{
    public class MakeReservationCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public Guid ReserveeId { get; }

        public string EmployeeName { get; }

        public string ServiceName { get; }

        public decimal ServicePrice { get; }

        public int ServiceDuration { get; }

        public DateTime Start { get; }

        public MakeReservationCommand(
            Guid companyId,
            Guid employeeId,
            Guid reserveeId,
            string employeeName,
            string serviceName,
            decimal servicePrice,
            int serviceDuration,
            DateTime start)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            ReserveeId = reserveeId;
            EmployeeName = employeeName;
            ServiceName = serviceName;
            ServicePrice = servicePrice;
            ServiceDuration = serviceDuration;
            Start = start;
        }
    }
}
