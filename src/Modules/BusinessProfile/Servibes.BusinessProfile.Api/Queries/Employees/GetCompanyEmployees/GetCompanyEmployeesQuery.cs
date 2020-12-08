using MediatR;
using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees
{
    public class GetCompanyEmployeesQuery : IRequest<IEnumerable<CompanyEmployeeDto>>
    {
        public Guid CompanyId { get; set; }
    }
}
