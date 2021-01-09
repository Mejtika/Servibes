using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Availability.Application.Employees.GetEmployeeTimeOffs
{
    public class GetEmployeeTimeOffsQuery : IRequest<List<EmployeeTimeOffDto>>
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public GetEmployeeTimeOffsQuery(Guid companyId, Guid employeeId)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
        }
    }
}