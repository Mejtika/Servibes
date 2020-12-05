using System;
using System.Collections.Generic;
using FluentAssertions;
using Servibes.Availability.Core;
using Servibes.Availability.Core.Events;
using Servibes.Shared.BuildingBlocks;
using Xunit;

namespace Servibes.Availability.UnitTests
{
    public class EmployeeTest
    {
        [Fact]
        public void ShouldCreateEmployee()
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var hoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var weekWorkingHours = WeekHoursRange.Create(hoursRanges);

            var employee = Employee.Create(employeeId, companyId, weekWorkingHours);

            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeAvailabilityCreated>();
        }

        [Fact]
        public void ShouldChangeEmployeeWorkingHours()
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var companyHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };

            var newEmployeeHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(15), TimeSpan.FromHours(16)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(14), TimeSpan.FromHours(15)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(16), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
            };

            var newEmployeeWorkingHours = WeekHoursRange.Create(newEmployeeHoursRanges);
            var companyOpeningHours = WeekHoursRange.Create(companyHoursRanges);

            var company = Company.Create(companyId, companyOpeningHours);
            var employee = Employee.Create(employeeId, companyId, companyOpeningHours);

            employee.ChangeWorkingHours(company.OpeningHours, newEmployeeWorkingHours);

            employee.WorkingHours.Should().Be(newEmployeeWorkingHours);
        }
    }
}
