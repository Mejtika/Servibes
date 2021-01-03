using System;
using System.Collections.Generic;
using MediatR;
using Servibes.Availability.Application.Shared;

namespace Servibes.Availability.Application.Employees.ChangeWorkingHours
{
    public class ChangeWorkingHoursCommand : IRequest
    {
        public Guid CompanyId { get; }
        public Guid EmployeeId { get; }
        public List<HoursRangeDto> WorkingHours { get; }

        public ChangeWorkingHoursCommand(Guid companyId, Guid employeeId, List<HoursRangeDto> workingHours)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            WorkingHours = workingHours;
        }
    }
}
