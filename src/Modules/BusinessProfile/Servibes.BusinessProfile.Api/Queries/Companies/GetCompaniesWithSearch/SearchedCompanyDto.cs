using System;
using System.Collections.Generic;
using Servibes.BusinessProfile.Api.Queries.Services;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompaniesWithSearch
{
    public class SearchedCompanyDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public List<CompanyServiceDto> Services { get; set; }
        public CompanyAddressDto Address { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhotoId { get; set; }
    }
}