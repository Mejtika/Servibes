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
        {
            return StartDateIsEarlierThanEndDate() &&
                   StartDateIsInFuture() &&
                   EndDateIsInFuture() &&
                   ReservationIsShorterThanOneDay();

            bool StartDateIsEarlierThanEndDate() => start < end;
            bool StartDateIsInFuture() => start > now;
            bool EndDateIsInFuture() => start > now;
            bool ReservationIsShorterThanOneDay() => (end - start).TotalHours < 24;
        }

        public bool IsInReservationTime(DateTime date) => Start <= date && End >= date;

        public bool HasPassed(DateTime now) => End < now;
    }
}