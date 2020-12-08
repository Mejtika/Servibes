using System;

namespace Servibes.BusinessProfile.Api.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}