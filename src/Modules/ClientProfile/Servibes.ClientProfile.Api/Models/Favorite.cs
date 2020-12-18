using System;

namespace Servibes.ClientProfile.Api.Models
{
    public class Favorite
    {
        public Guid CompanyId { get; set; }

        public Guid ClientId { get; set; }
    }
}