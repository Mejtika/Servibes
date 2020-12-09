using System;
using System.Collections.Generic;
using FluentAssertions;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Companies.Events;
using Servibes.Availability.Core.Shared;
using Xunit;

namespace Servibes.Availability.UnitTests
{
    public class CompanyTests
    {
        [Fact]
        public void CompanyOpeningHoursCanBeChanged()
        {
            var companyId = Guid.NewGuid();
            var companyOpeningHours = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var company = Company.Create(companyId, companyOpeningHours);
            company.ClearDomainEvents();

            var newOpeningHours = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(15), TimeSpan.FromHours(16)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(19)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(14), TimeSpan.FromHours(15)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(16), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(10), TimeSpan.FromHours(17)),
            };
            company.ChangeOpeningHours(newOpeningHours);

            company.DomainEvents.Should().ContainSingle();
            company.DomainEvents.Should().AllBeOfType<CompanyOpeningHoursChanged>();
        }
    }
}
