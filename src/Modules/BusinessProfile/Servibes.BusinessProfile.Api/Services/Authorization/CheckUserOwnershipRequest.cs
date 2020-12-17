using System;

namespace Servibes.BusinessProfile.Api.Services
{
    public class CheckUserOwnershipRequest
    {
        public Guid UserId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
