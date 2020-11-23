using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Models
{
    public class CreateProfileDto
    {
        public string BusinessName { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }

        public List<Employee> Employees { get; set; }
        public List<OpenHours> OpenHours { get; set; }
        public List<Service> Services { get; set; }
    }
}
