﻿using Servibes.BusinessProfile.Api.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Model
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string Category { get; set; }
        public string CoverPhoto { get; set; }
        public Address Address { get; set; }
        public WeekHoursRange OpeningHours { get; set; }
    }
}
