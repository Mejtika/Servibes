using System;
using System.Collections.Generic;
using MediatR;
using Servibes.Availability.Application.Shared;

namespace Servibes.Availability.Application.Employees.GetEmployeeWorkingHours
{
    public class GetEmployeeWorkingHoursQuery : IRequest<List<HoursRangeDto>>
    {
        public Guid CompanyId { get; }
        public Guid EmployeeId { get; }

        public GetEmployeeWorkingHoursQuery(Guid employeeId, Guid companyId)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}
