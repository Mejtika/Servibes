using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Servibes.BusinessProfile.Api.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CompanyId { get; set; }
        public WeekHoursRange WorkingHours { get; set; }
    }
}