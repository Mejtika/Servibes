using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Dto
{
    public class AddressDto
    {
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}
