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

        public List<EmployeeDto> Employees { get; set; }
        public List<OpenHoursDto> OpeningHours { get; set; }
        public List<ServiceDto> Services { get; set; }
    }
}
