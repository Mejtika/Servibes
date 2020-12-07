using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees
{
    public class GetCompanyEmployeesQuery : IRequest<IEnumerable<CompanyEmployeeDto>>
    {
        public Guid CompanyId { get; set; }
    }
}
