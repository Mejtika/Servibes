using System;
using System.Collections.Generic;
using System.Linq;
using Servibes.Availability.Core.Employees.Events;
using Servibes.Availability.Core.Employees.Exceptions;
using Servibes.Availability.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees
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
            if (!AreDatesCorrect(start, end, now))
            {
                throw new IncorrectDatesException(nameof(Reservation),start, end);
            }
            return new Reservation(start, end);
        }

        public bool IsInWeekWorkingRange(IEnumerable<HoursRange> hoursRanges)
        {
            var companyHourRange = hoursRanges.SingleOrDefault(x => x.DayOfWeek == Start.DayOfWeek);
            var reservationHoursRange = ToHoursRange();
            return reservationHoursRange.IsWithin(companyHourRange);
        }

        public bool IsCollidingWith(Reservation reservation)
            => !(reservation.End <= Start || reservation.Start >= End);

        private HoursRange ToHoursRange()
            => HoursRange.Create(Start.DayOfWeek, true, Start.TimeOfDay, End.TimeOfDay);

        private static bool AreDatesCorrect(DateTime start, DateTime end, DateTime now)
        {
            return StartDateIsEarlierThanEndDate() &&
                   StartDateIsInFuture() &&
                   EndDateIsInFuture() &&
                   ReservationIsShorterThanOneDay();
           
            bool StartDateIsEarlierThanEndDate() => start < end;
            bool StartDateIsInFuture() => start > now;
            bool EndDateIsInFuture() => start > now;
            bool ReservationIsShorterThanOneDay() => start.Date == end.Date;
        }
    }
}