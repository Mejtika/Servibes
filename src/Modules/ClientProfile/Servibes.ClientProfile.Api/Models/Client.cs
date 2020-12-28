using System;
using System.Collections.Generic;

namespace Servibes.ClientProfile.Api.Models
{
    public class Client
    {
        public Guid ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
