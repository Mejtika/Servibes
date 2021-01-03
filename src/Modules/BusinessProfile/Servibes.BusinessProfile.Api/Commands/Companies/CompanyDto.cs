using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Servibes.BusinessProfile.Api.Commands.Services;

namespace Servibes.BusinessProfile.Api.Commands.Companies
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhotoId { get; set; }
        public CompanyAddressDto Address { get; set; }
        public List<EmployeeDto> Employees { get; set; }
        public List<HoursRangeDto> OpeningHours { get; set; }
        public List<ServiceDto> Services { get; set; }
    }
}
