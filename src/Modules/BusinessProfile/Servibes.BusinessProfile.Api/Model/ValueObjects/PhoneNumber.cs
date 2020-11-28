using Servibes.BusinessProfile.Api.Model.Enumerations;
using Servibes.Shared.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Servibes.BusinessProfile.Api.Model.ValueObjects
{
    public class PhoneNumber
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new InvalidOperationException(phoneNumber);
            }

            if (!Regex.IsMatch(phoneNumber, @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new InvalidOperationException(phoneNumber);
            }

            return new PhoneNumber(phoneNumber.ToLowerInvariant());
        }
    }
}
