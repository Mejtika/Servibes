using System;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Models.ClientBase
{
    public class Client
    {
        public Guid ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}