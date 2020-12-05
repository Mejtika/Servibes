﻿using System.Collections.Generic;
using System.Text;
using Servibes.Shared.Enumerations;

namespace Servibes.BusinessProfile.Api.Models
{
    public class HoursRangeDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
