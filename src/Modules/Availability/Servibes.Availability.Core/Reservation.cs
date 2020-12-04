using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
{
    public class Reservation : ValueObject
    {
        public static Reservation Create(DateTime start, DateTime end, DateTime now)
        {
            if(AreDatesCorrect(start, end, now))
            {
                throw new Exception();
            }
            return new Reservation(start, end);
        }

        public DateTime Start { get; }
        public DateTime End { get; }

        private Reservation(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool IsLongPeriodReservation() => (End - Start).Hours > 12;
 
        public bool IsCollidingWith(Reservation reservation) 
            => !(reservation.End <= Start || reservation.Start >= End);

        private static bool AreDatesCorrect(DateTime start, DateTime end, DateTime now)
            => (start < end) && (start > now) && (end > now);
    }
}