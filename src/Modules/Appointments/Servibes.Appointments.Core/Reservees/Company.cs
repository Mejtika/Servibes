using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Reservees
{
    public class Company : IAggregateRoot
    {
        public Guid CompanyId { get; private set; }

        public Guid WalkInId { get; private set; }

        public Guid OwnerId { get; private set; }

        private Company(Guid companyId, Guid walkInId, Guid ownerId)
        {
            CompanyId = companyId;
            WalkInId = walkInId;
            OwnerId = ownerId;
        }

        public static Company Create(Guid companyId, Guid walkInId, Guid ownerId)
        {
            return new Company(companyId, walkInId, ownerId);
        }
    }
}
