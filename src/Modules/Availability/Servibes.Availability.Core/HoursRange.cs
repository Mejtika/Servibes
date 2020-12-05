using System;
using System.Diagnostics;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
{
    public class HoursRange : ValueObject
    {
        public DayOfWeek DayOfWeek { get; }
        public bool IsAvailable { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        private HoursRange(bool isAvailable, TimeSpan start, TimeSpan end, DayOfWeek day)
        {
            IsAvailable = isAvailable;
            Start = start;
            End = end;
            DayOfWeek = day;
        }

        public static HoursRange Create(DayOfWeek day, bool isAvailable, TimeSpan start, TimeSpan end)
        {
            if (end < start)
                throw new Exception();
            return new HoursRange(isAvailable, start, end, day);
        }

        public bool IsCollidingWith(HoursRange other)
            => DayOfWeek == other.DayOfWeek && IsAvailable == other.IsAvailable && IsAvailable &&
               !(other.End <= Start || other.Start >= End);

        public bool IsWithin(HoursRange other)
            =>
            DayOfWeek == other.DayOfWeek && IsAvailable == other.IsAvailable &&
                   (End <= other.End && Start >= other.Start);
    }
}