using System;
using System.Collections.Generic;
using System.Linq;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
{
    public class Reservation : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private Reservation(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static Reservation Create(DateTime start, DateTime end, DateTime now)
        {
            if (AreDatesCorrect(start, end, now))
            {
                throw new Exception();
            }
            return new Reservation(start, end);
        }

        public bool IsInWeekWorkingRange(IEnumerable<HoursRange> hoursRanges)
            => hoursRanges.SingleOrDefault(x => x.DayOfWeek == Start.DayOfWeek)
                .IsCollidingWith(ToHoursRange());

        public bool IsCollidingWith(Reservation reservation)
            => !(reservation.End <= Start || reservation.Start >= End);

        private HoursRange ToHoursRange()
            => HoursRange.Create(Start.DayOfWeek, true, Start.TimeOfDay, End.TimeOfDay);

        private static bool AreDatesCorrect(DateTime start, DateTime end, DateTime now)
            => (start < end) && (start > now) && (end > now) && start.DayOfWeek == end.DayOfWeek;
    }
}