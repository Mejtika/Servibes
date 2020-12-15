using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.BusinessProfile.Api.Models.ClientBase;

namespace Servibes.BusinessProfile.Api.Events.External.NewClientRegistered
{
    public class NewClientRegisteredEventHandler : INotificationHandler<NewClientRegisteredEvent>
    {
        private readonly BusinessProfileContext _context;

        public NewClientRegisteredEventHandler(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task Handle(NewClientRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                ClientId = notification.ClientId,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
                Email = notification.Email
            };

            await _context.Clients.AddAsync(client, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}