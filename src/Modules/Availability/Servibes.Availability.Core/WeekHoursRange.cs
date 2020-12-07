using System;
using System.Collections.Generic;
using System.Linq;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
{
    public class WeekHoursRange : ValueObject
    {
        public static WeekHoursRange Create(IEnumerable<HoursRange> weekHoursRanges)
        {
            CheckForDaysCorrectness(weekHoursRanges);
            return new WeekHoursRange(weekHoursRanges);
        }

        public IEnumerable<HoursRange> HoursRanges { get; }

        public HoursRange GetByDayOfWeek(DayOfWeek dayOfWeek)
            => HoursRanges.SingleOrDefault(x => x.DayOfWeek == dayOfWeek);
 
        private WeekHoursRange(IEnumerable<HoursRange> weekHoursRanges)
        {
            HoursRanges = weekHoursRanges;
        }

        private static void CheckForDaysCorrectness(IEnumerable<HoursRange> weekHoursRanges)
        {
            if (weekHoursRanges.ToList().Count != Enum.GetNames(typeof(DayOfWeek)).Length)
            {
                throw new Exception("Wrong number of week days.");
            }

            var daysOfTheWeek = Enum.GetNames(typeof(DayOfWeek)).ToList();
            var daysInWorkignHours = weekHoursRanges.Select(x => x.DayOfWeek.ToString()).ToList();
            if (daysOfTheWeek.Except(daysInWorkignHours).Any())
            {
                throw new Exception("Missing some week day.");
            }
        }
    }
}