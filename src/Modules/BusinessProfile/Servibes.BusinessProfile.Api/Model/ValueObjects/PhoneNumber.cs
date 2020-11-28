using Servibes.BusinessProfile.Api.Model.Enumerations;
using Servibes.Shared.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Model.ValueObjects
{
    public class PhoneNumber// : ValueObject
    {
        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }

        public PhoneNumber() { }

        public PhoneNumber(string number, PhoneType phoneType)
        {
            this.Number = number;
            this.PhoneType = phoneType;
        }

        /*protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
            yield return PhoneType;
        }*/
    }
}
