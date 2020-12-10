using System;
using System.Collections.Generic;
using FluentAssertions;
using Servibes.Appointments.Core.Shared;
using Xunit;

namespace Servibes.Appointments.UnitTests
{
    public class ReservationDateTests
    {
        public static List<object[]> CorrectReservationDates()
        {
            return new List<object[]>
            {
                new object[] { DateTime.Today.AddHours(12).AddMinutes(30), DateTime.Today.AddHours(14), DateTime.Today.AddHours(11) },
                new object[] { DateTime.Today.AddHours(18).AddMinutes(45), DateTime.Today.AddHours(19), DateTime.Today.AddHours(18).AddMinutes(44) },
                new object[] { DateTime.Today.AddHours(13), DateTime.Today.AddHours(14), DateTime.Today.AddDays(-5) },
                new object[] { DateTime.Today.AddDays(7).AddHours(12), DateTime.Today.AddDays(7).AddHours(15), DateTime.Today.AddHours(12) },
                new object[] { DateTime.Today.AddDays(30).AddHours(12), DateTime.Today.AddDays(30).AddHours(13), DateTime.Today.AddDays(30).AddHours(10) },
                new object[] { DateTime.Today.AddHours(1), DateTime.Today.AddHours(2), DateTime.Today.AddMinutes(30) }
            };
        }

        public static List<object[]> IncorrectReservationDates()
        {
            return new List<object[]>
            {
                new object[] { DateTime.Today.AddHours(12).AddMinutes(30), DateTime.Today.AddHours(11), DateTime.Today.AddHours(10) },
                new object[] { DateTime.Today.AddHours(18).AddMinutes(45), DateTime.Today.AddHours(19), DateTime.Today.AddHours(18).AddMinutes(46) },
                new object[] { DateTime.Today.AddHours(13), DateTime.Today.AddHours(14), DateTime.Today.AddDays(5) },
                new object[] { DateTime.Today.AddDays(-7).AddHours(12), DateTime.Today.AddDays(-7).AddHours(15), DateTime.Today.AddHours(10) },
                new object[] { DateTime.Today.AddDays(21).AddHours(12), DateTime.Today.AddDays(30).AddHours(13), DateTime.Today.AddDays(20).AddHours(10) },
                new object[] { DateTime.Today.AddHours(1), DateTime.Today.AddHours(1), DateTime.Today.AddMinutes(30) }
            };
        }

        [Theory]
        [MemberData(nameof(CorrectReservationDates))]
        public void ReservationDateCanBeCreatedFromCorrectDates(DateTime start, DateTime end, DateTime now)
        {
            var reservationDate = ReservationDate.Create(start, end, now);
            reservationDate.Start.Should().Be(start);
            reservationDate.End.Should().Be(end);
        }

        [Theory]
        [MemberData(nameof(IncorrectReservationDates))]
        public void ReservationDateCannotBeCreatedFromIncorrectDates(DateTime start, DateTime end, DateTime now)
        {
            Func<ReservationDate> creatingReservationDate = () => ReservationDate.Create(start, end, now);

            creatingReservationDate.Should().Throw<ReservationDatesAreIncorrectException>()
                .WithMessage($"Reservation date {start} - {end} are not correct.");
        }
    }
}
