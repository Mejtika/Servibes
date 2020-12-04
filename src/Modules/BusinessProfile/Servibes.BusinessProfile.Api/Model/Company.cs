using System;

namespace Servibes.BusinessProfile.Api.Model
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Address Address { get; set; }
        public string Category { get; set; }
        public string CoverPhoto { get; set; }
    }
}
