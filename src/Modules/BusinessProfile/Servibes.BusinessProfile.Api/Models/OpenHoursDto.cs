using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Models
{
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturaday,
        Sunday
    }

    public class OpenHoursDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsActive { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
    }
}
