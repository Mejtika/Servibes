using System;
using System.Collections.Generic;
using System.Text;
using Servibes.BusinessProfile.Api.Model.Enumerations;
using DayOfWeek = Servibes.BusinessProfile.Api.Model.Enumerations.DayOfWeek;

namespace Servibes.BusinessProfile.Api.Model
{
    public class HoursRange
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsOpen { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
    }
}
