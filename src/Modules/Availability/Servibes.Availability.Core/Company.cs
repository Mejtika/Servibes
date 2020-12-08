using System;
using System.Collections.Generic;
using System.Text;
using Servibes.Availability.Core.Events;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
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
            return new Company(CompanyId, openingHours);
        }

        public void ChangeOpeningHours(WeekHoursRange openingHours)
        {
            OpeningHours = openingHours;
            AddDomainEvent(new CompanyOpeningHoursChanged(this));
        }
    }
}
