using System.Collections.Generic;
using System.Text;
using System;
using DayOfWeek = Servibes.Shared.Enumerations.DayOfWeek;

namespace Servibes.BusinessProfile.Api.Model
{
    public class OpeningHours
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsOpen { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
