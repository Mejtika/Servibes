using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Availability.Application.Employees.GetTimeOffs
{
    public class GetTimeOffsCommand : IRequest<List<TimeOffDto>>
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public GetTimeOffsCommand(Guid companyId, Guid employeeId)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
        }
    }
}