using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Servibes.Availability.Core;
using Xunit;

namespace Servibes.Availability.UnitTests
{
    public class HoursRangeTests
    {
        public static List<object[]> TestHoursRangesData()
        {
            return new List<object[]>
            {
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,12, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Friday, new TimeSpan(0,11, 0, 0) , new TimeSpan(0, 16, 0, 0), false},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,14, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Thursday, new TimeSpan(0,13, 0, 0) , new TimeSpan(0, 13, 59, 59), false},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,14, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Thursday, new TimeSpan(0,14, 30, 0) , new TimeSpan(0, 15, 50, 59), false},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,12, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Monday, new TimeSpan(0,11, 0, 0) , new TimeSpan(0, 17, 0, 1), true},
                new object[] { false, DayOfWeek.Sunday, new TimeSpan(0,15, 0, 0) , new TimeSpan(0, 22, 30, 0),
                    true, DayOfWeek.Sunday, new TimeSpan(0,13, 0, 0) , new TimeSpan(0, 14, 30, 0), false},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,10, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Monday, new TimeSpan(0,9, 0, 0) , new TimeSpan(0, 16, 0, 0), true},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,12, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    false, DayOfWeek.Monday, new TimeSpan(0,13, 0, 0) , new TimeSpan(0, 14, 32, 23), false},
                new object[] { true, DayOfWeek.Thursday, new TimeSpan(0,12, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Thursday, new TimeSpan(0,18, 5, 0) , new TimeSpan(0, 22, 45, 0), false},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,18, 30, 0) , new TimeSpan(0, 19, 45, 0),
                    true, DayOfWeek.Monday, new TimeSpan(0,11, 0, 0) , new TimeSpan(0, 16, 0, 0), false},
                new object[] { true, DayOfWeek.Monday, new TimeSpan(0,14, 0, 0) , new TimeSpan(0, 18, 0, 0),
                    true, DayOfWeek.Monday, new TimeSpan(0,13, 50, 50) , new TimeSpan(0, 13, 58, 1), false},
            };
        }

        [Theory]
        [MemberData(nameof(TestHoursRangesData))]
        public void ShouldCheckForCollision(bool baseAvailability, DayOfWeek baseDay, TimeSpan baseStart, TimeSpan baseEnd, 
            bool otherAvailability, DayOfWeek otherDay, TimeSpan otherStart, TimeSpan otherEnd, bool expected)
        {
            var baseHoursRange = HoursRange.Create(baseDay, baseAvailability, baseStart, baseEnd);
            var otherHoursRange = HoursRange.Create(otherDay, otherAvailability, otherStart, otherEnd);

            var result = baseHoursRange.IsCollidingWith(otherHoursRange);

            result.Should().Be(expected);
        }
    }
}
