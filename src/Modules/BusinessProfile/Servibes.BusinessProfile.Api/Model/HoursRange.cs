using System;

namespace Servibes.BusinessProfile.Api.Model
{
    public class HoursRange
    {
        public static HoursRange Create(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            return new HoursRange(isAvailable, start, end);
        }

        private HoursRange(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            IsAvailable = isAvailable;
            Start = start;
            End = end;
        }

        public bool IsAvailable { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
    }
}
