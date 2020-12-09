using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Shared
{
    public class HoursRange : ValueObject
    {
        public DayOfWeek DayOfWeek { get; }
        public bool IsAvailable { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        private HoursRange(bool isAvailable, TimeSpan start, TimeSpan end, DayOfWeek dayOfWeek)
        {
            IsAvailable = isAvailable;
            Start = start;
            End = end;
            DayOfWeek = dayOfWeek;
        }

        public static HoursRange Create(DayOfWeek dayOfWeek, bool isAvailable, TimeSpan start, TimeSpan end)
        {
            if (end < start)
                throw new Exception();
            return new HoursRange(isAvailable, start, end, dayOfWeek);
        }

        public bool IsCollidingWith(HoursRange other)
        {
            return DayOfTheWeeksAreMatching() &&
                   DaysAvailabilityAreMatching() &&
                   HoursRangesAreColliding();

            bool DayOfTheWeeksAreMatching() => DayOfWeek == other.DayOfWeek;
            bool DaysAvailabilityAreMatching() => IsAvailable == other.IsAvailable && IsAvailable;
            bool HoursRangesAreColliding() => !(other.End <= Start || other.Start >= End);
        }

        public bool IsWithin(HoursRange other)
        {
            return DayOfTheWeeksAreMatching() &&
                   DaysAvailabilityAreMatching() &&
                   HoursRangesAreWithin();

            bool DayOfTheWeeksAreMatching() => DayOfWeek == other.DayOfWeek;
            bool DaysAvailabilityAreMatching() => IsAvailable == other.IsAvailable;
            bool HoursRangesAreWithin() => End <= other.End && Start >= other.Start;
        }

        public bool IsWithin(TimeSpan otherStart, TimeSpan otherEnd)
        {
            return HoursRangesAreWithin();

            bool HoursRangesAreWithin() => End <= otherEnd && Start >= otherStart;
        }
    }
}