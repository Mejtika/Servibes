using System;
using MediatR;

namespace Servibes.ClientProfile.Api.Events.External.ClientInformationUpdated
{
    public class ClientInformationUpdatedEvent : INotification
    {
        public Guid ClientId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public ClientInformationUpdatedEvent(
            string clientId,
            string firstName,
            string lastName,
            string email)
        {
            ClientId = Guid.Parse(clientId);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
