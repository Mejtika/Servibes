using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servibes.BusinessProfile.Api.Model
{
    public class HoursRange
    {
        public HoursRange(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            IsAvailable = isAvailable;
            Start = start;
            End = end;
        }

        public bool IsAvailable { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
    }

    public class WeekHoursRange
    {
        public WeekHoursRange(
            HoursRange monday,
            HoursRange tuesday,
            HoursRange wednesday,
            HoursRange thursday,
            HoursRange friday,
            HoursRange saturday,
            HoursRange sunday)
        {
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }

        public HoursRange Monday { get; private set; }
        public HoursRange Tuesday { get; private set; }
        public HoursRange Wednesday { get; private set; }
        public HoursRange Thursday { get; private set; }
        public HoursRange Friday { get; private set; }
        public HoursRange Saturday { get; private set; }
        public HoursRange Sunday { get; private set; }
    }
}
