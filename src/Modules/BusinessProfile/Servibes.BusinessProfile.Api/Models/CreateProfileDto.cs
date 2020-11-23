using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Models
{
    public class CreateProfileDto
    {
        public string BusinessName { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string Category { get; set; }
        public string CoverPhoto { get; set; }
        public string Location { get; set; }

        public List<Employee> Employees { get; set; }
        public List<OpenHours> OpeningHours { get; set; }
        public List<Service> Services { get; set; }
    }
}
