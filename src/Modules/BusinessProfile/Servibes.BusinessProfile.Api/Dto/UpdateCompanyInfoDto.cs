using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Dto
{
    public class UpdateCompanyInfoDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        public AddressDto Address { get; set; }
    }
}
