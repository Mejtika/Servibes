using System;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
