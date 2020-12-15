using System;
using MediatR;

namespace Servibes.Appointments.Application.Events.External.NewClientRegistered
{
    public class NewClientRegisteredEvent : INotification
    {
        public Guid ClientId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public NewClientRegisteredEvent(
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
