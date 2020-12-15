using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Reservees
{
    public class Company : IAggregateRoot
    {
        public Guid CompanyId { get; private set; }

        public Guid WalkInId { get; private set; }

        private Company(Guid companyId, Guid walkInId)
        {
            CompanyId = companyId;
            WalkInId = walkInId;
        }

        public static Company Create(Guid companyId, Guid walkInId)
        {
            return new Company(companyId, walkInId);
        }
    }
}
