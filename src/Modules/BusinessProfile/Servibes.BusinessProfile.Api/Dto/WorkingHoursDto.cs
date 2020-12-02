using System;
using System.Collections.Generic;
using System.Text;
using DayOfWeek = Servibes.Shared.Enumerations.DayOfWeek;

namespace Servibes.BusinessProfile.Api.Dto
{
    public class WorkingHoursDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
    }
}
