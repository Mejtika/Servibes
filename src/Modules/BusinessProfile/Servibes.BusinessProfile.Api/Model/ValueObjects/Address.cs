using Servibes.Shared.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Model.ValueObjects
{
    public class Address// : ValueObject
    {
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string FlatNumber { get; private set; }

        public Address() { }

        public Address(string city, string zipCode, string street, string streetNumber, string flatNumber)
        {
            this.City = city;
            this.ZipCode = zipCode;
            this.Street = street;
            this.StreetNumber = streetNumber;
            this.FlatNumber = flatNumber;
        }

        /*protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return ZipCode;
            yield return Street;
            yield return StreetNumber;
            yield return FlatNumber;
        }*/
    }
}
