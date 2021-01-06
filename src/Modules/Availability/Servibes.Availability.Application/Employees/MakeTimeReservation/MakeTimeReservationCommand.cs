using System;
using MediatR;

namespace Servibes.Availability.Application.Employees.MakeTimeReservation
{
    public class MakeTimeReservationCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public int Duration { get; }

        public MakeTimeReservationCommand(
            Guid companyId,
            Guid employeeId,
            DateTime start,
            int duration)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            Duration = duration;
        }
    }
}
