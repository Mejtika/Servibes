using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Reservees
{
    public class Client : IAggregateRoot
    {
        public Guid ClientId { get; private set; }

        private Client(Guid clientId)
        {
            ClientId = clientId;
        }

        public static Client Create(Guid clientId)
        {
            return new Client(clientId);
        }
    }
}
