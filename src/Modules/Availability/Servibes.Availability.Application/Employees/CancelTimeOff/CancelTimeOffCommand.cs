using System;
using MediatR;

namespace Servibes.Availability.Application.Employees.CancelTimeOff
{
    public class CancelTimeOffCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public CancelTimeOffCommand(Guid companyId, Guid employeeId, DateTime start)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
        }
    }
}