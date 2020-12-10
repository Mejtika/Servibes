using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Companies.Events
{
    public class CompanyOpeningHoursChanged : IDomainEvent
    {
        public Company Company { get; }

        public CompanyOpeningHoursChanged(Company company)
        {
            Company = company;
        }
    }
}