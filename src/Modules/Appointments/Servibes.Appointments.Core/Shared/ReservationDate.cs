using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Shared
{
    public class ReservationDate : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private ReservationDate(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static ReservationDate Create(DateTime start, DateTime end, DateTime now)
        {
            if (!AreDatesCorrect(start, end, now))
            {
                throw new ReservationDatesAreIncorrectException(start, end);
            }
            return new ReservationDate(start, end);
        }

        private static bool AreDatesCorrect(DateTime start, DateTime end, DateTime now)
            => (start < end) && (start > now) && (end > now) && start.DayOfWeek == end.DayOfWeek;

        public bool IsInReservationTime(DateTime date) => Start <= date && End >= date;

        public bool HasPassed(DateTime now) => End < now;
    }
}