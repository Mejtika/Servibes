using Servibes.BusinessProfile.Api.Dto;
using Servibes.BusinessProfile.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Models
{
    public class CreateProfileDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        public AddressDto Address { get; set; }

        public List<EmployeeDto> Employees { get; set; }
        public List<HoursRangeDto> OpeningHours { get; set; }
        public List<ServiceDto> Services { get; set; }
    }
}
