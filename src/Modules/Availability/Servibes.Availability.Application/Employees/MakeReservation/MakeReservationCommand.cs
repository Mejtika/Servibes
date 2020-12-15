using System;
using MediatR;

namespace Servibes.Availability.Application.Employees.MakeReservation
{
    public class MakeReservationCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public Guid ReserveeId { get; }

        public Guid ServiceId { get; }

        public DateTime Start { get; }

        public MakeReservationCommand(
            Guid companyId,
            Guid employeeId,
            Guid reserveeId,
            Guid serviceId,
            DateTime start)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            ReserveeId = reserveeId;
            ServiceId = serviceId;
            Start = start;
        }
    }
}
