using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees
{
    public class CompanyEmployeesDto
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
