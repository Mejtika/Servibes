using System;
using Servibes.Availability.Core.Companies.Events;
using Servibes.Availability.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Companies
{
    public class Company : Entity, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }

        public WeekHoursRange OpeningHours { get; private set; }

        private Company(Guid companyId, WeekHoursRange openingHours)
        {
            CompanyId = companyId;
            OpeningHours = openingHours;
        }

        public static Company Create(Guid CompanyId, WeekHoursRange openingHours)
        {
            var company = new Company(CompanyId, openingHours);
            company.AddDomainEvent(new CompanyAvailabilityCreated(company));
            return company;
        }

        public void ChangeOpeningHours(WeekHoursRange openingHours)
        {
            OpeningHours = openingHours;
            AddDomainEvent(new CompanyOpeningHoursChanged(this));
        }
    }
}
