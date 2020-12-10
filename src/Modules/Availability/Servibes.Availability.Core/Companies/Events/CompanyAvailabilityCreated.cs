using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Companies.Events
{
    public class CompanyAvailabilityCreated : IDomainEvent
    {
        public Company Company { get; }

        public CompanyAvailabilityCreated(Company company)
        {
            Company = company;
        }
    }
}