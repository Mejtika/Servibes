using System;
using System.Collections.Generic;
using System.Linq;

namespace Servibes.Availability.Core
{
    public class HoursRange
    {
        public HoursRange(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            IsAvailable = isAvailable;
            Start = start;
            End = end;
        }

        public bool IsAvailable { get; }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        public bool IsCollidingWith(TimeSpan start, TimeSpan end)
            => !IsAvailable || !(end <= Start || start >= End);
    }

    public class WorkingHours
    {
        public WorkingHours(
            HoursRange monday,
            HoursRange tuesday,
            HoursRange wednesday,
            HoursRange thursday,
            HoursRange friday,
            HoursRange saturday,
            HoursRange sunday)
        {
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }

        private HoursRange Monday { get; }
        private HoursRange Tuesday { get; }
        private HoursRange Wednesday { get; }
        private HoursRange Thursday { get; }
        private HoursRange Friday { get; }
        private HoursRange Saturday { get; }
        private HoursRange Sunday { get; }

        public bool IsOutOfRange(Reservation reservation)
            => reservation.Start.DayOfWeek switch
            {
                DayOfWeek.Monday => Monday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                DayOfWeek.Tuesday => Tuesday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                DayOfWeek.Wednesday => Wednesday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                DayOfWeek.Thursday => Thursday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                DayOfWeek.Friday => Friday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                DayOfWeek.Saturday => Saturday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                DayOfWeek.Sunday => Sunday.IsCollidingWith(reservation.Start.TimeOfDay, reservation.End.TimeOfDay),
                _ => throw new ArgumentOutOfRangeException()
            };
    }


    //private static void CheckForDaysCorrectness(IEnumerable<WorkingHours> workingHours)
    //{
    //    if (workingHours.ToList().Count != Enum.GetNames(typeof(DayOfWeek)).Length)
    //    {
    //        throw new Exception();
    //    }

    //    var daysOfTheWeek = Enum.GetNames(typeof(DayOfWeek)).ToList();
    //    var daysInWorkignHours = workingHours.Select(x => x.DayOfWeek.ToString()).ToList();
    //    if (daysOfTheWeek.Except(daysInWorkignHours).Any())
    //    {
    //        throw new Exception();
    //    }
    //}
}