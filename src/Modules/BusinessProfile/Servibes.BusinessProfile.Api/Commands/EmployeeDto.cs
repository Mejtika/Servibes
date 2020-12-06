using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands
{
    public class EmployeeDto
    {
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
