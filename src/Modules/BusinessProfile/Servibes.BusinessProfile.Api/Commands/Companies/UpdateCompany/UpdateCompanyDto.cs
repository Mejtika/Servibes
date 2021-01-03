using Microsoft.AspNetCore.Http;
using System;

namespace Servibes.BusinessProfile.Api.Commands.Companies.UpdateCompany
{
    public class UpdateCompanyDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhotoId { get; set; }
        public CompanyAddressDto Address { get; set; }
    }
}
