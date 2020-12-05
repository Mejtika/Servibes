using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Company
{
    public class CompanyAddressDto
    {
        public string City { get; }
        public string ZipCode { get; }
        public string Street { get; }
        public string FlatNumber { get; }
        public string StreetNumber { get; }
    }
}
