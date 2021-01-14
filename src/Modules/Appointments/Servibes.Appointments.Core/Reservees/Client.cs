using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Reservees
{
    public class Client : IAggregateRoot
    {
        public Guid ClientId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        private Client(Guid clientId, string firstName, string lastName, string email)
        {
            ClientId = clientId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void Update(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string Name => $"{FirstName} {LastName}";

        public static Client Create(Guid clientId, string firstName, string lastName, string email)
        {
            return new Client(clientId, firstName, lastName, email);
        }
    }
}
