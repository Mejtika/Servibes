using Servibes.Shared.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Model.ValueObjects
{
    public class Address
    {
        public string City { get; }
        public string ZipCode { get; }
        public string Street { get; }
        public string FlatNumber { get;  }
        public string StreetNumber { get;  }

        private Address(string city, string zipCode, string street, string streetNumber, string flatNumber)
        {
            City = city;
            ZipCode = zipCode;
            Street = street;
            StreetNumber = streetNumber;
            FlatNumber = flatNumber;
        }

        public static Address Create(string city, string zipCode, string street, string streetNumber, string flatNumber)
        {
            return new Address(city, zipCode, street, streetNumber, flatNumber);
        }
    }
}
