using System;

namespace Servibes.BusinessProfile.Api.Queries.Companies
{
    public class CompanyDetailsDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyAddressDto Address { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
    }
}