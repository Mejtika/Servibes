using System;
using System.Collections.Generic;
using System.Linq;
using Servibes.Availability.Core.Companies.Events;
using Servibes.Availability.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Companies
{
    public class Company : Entity, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }

        private List<HoursRange> _openingHours;

        private Company()
        {
            _openingHours = new List<HoursRange>();
        }

        private Company(Guid companyId, List<HoursRange> openingHours)
        {
            CompanyId = companyId;
            _openingHours = openingHours;
        }

        public static Company Create(Guid companyId, List<HoursRange> openingHours)
        {
            CheckForDaysCorrectness(openingHours);
            var company = new Company(companyId, openingHours);
            company.AddDomainEvent(new CompanyAvailabilityCreated(company));
            return company;
        }

        public void ChangeOpeningHours(List<HoursRange> openingHours)
        {
            CheckForDaysCorrectness(openingHours);
            _openingHours = openingHours;
            AddDomainEvent(new CompanyOpeningHoursChanged(this));
        }

        private static void CheckForDaysCorrectness(List<HoursRange> weekHoursRanges)
        {
            if (weekHoursRanges.Count != Enum.GetNames(typeof(DayOfWeek)).Length)
            {
                throw new IncorrectHoursRangesException("Wrong number of week days.");
            }

            var daysOfTheWeek = Enum.GetNames(typeof(DayOfWeek)).ToList();
            var daysInWorkingHours = weekHoursRanges.Select(x => x.DayOfWeek.ToString()).ToList();
            if (daysOfTheWeek.Except(daysInWorkingHours).Any())
            {
                throw new IncorrectHoursRangesException("Missing some week day.");
            }
        }
    }
}
