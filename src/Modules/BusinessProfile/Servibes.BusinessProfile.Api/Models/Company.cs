using System;

namespace Servibes.BusinessProfile.Api.Models
{
    public class Company
    {
        public Guid CompanyId { get; set; }

        public string CompanyName { get; set; }

        public Guid OwnerId { get; set; }

        public Guid WalkInClientId { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public Address Address { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public Guid CoverPhotoId { get; set; }
    }
}
