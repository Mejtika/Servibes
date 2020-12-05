using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees
{
    public class GetCompanyEmployeesQuery : IRequest<IEnumerable<CompanyEmployeesDto>>
    {
        public Guid CompanyId { get; set; }
    }
}
