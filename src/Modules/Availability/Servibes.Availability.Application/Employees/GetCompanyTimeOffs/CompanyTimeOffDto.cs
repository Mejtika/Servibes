using System;

namespace Servibes.Availability.Application.Employees.GetCompanyTimeOffs
{
    public class CompanyTimeOffDto
    {
        public Guid EmployeeId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}