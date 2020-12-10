using System;
using Servibes.Availability.Core.Employees.Events;
using Servibes.Availability.Core.Employees.Exceptions;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees
{
    public class TimeOff : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private TimeOff(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static TimeOff Create(DateTime start, DateTime end, DateTime now)
        {
            if (!AreDatesCorrect(start, end, now))
            {
                throw new IncorrectDatesException(nameof(TimeOff), start, end);
            }
            return new TimeOff(start, end);
        }

        public bool IsCollidingWith(TimeOff timeOff)
            => this.IsCollidingWith(timeOff.Start, timeOff.End);

        public bool IsCollidingWith(Reservation reservation)
            => this.IsCollidingWith(reservation.Start, reservation.End);

        private bool IsCollidingWith(DateTime start, DateTime end)
            => !(end.Date <= Start.Date || start.Date >= End.Date);

        private static bool AreDatesCorrect(DateTime start, DateTime end, DateTime now)
        {
            return StartDateIsEarlierThanEndDate() &&
                   StartDateIsInFuture() &&
                   EndDateIsInFuture() &&
                   ReservationIsLongerThanOneDay();

            bool StartDateIsEarlierThanEndDate() => start < end;
            bool StartDateIsInFuture() => start > now;
            bool EndDateIsInFuture() => start > now;
            bool ReservationIsLongerThanOneDay() => (end - start).Days >= 1;
        }
    }
}