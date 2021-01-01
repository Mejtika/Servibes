using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Sales.Api;
using Servibes.Sales.Api.Events.External.NewClientRegistered;
using Servibes.Sales.Api.Models;

namespace Servibes.BusinessProfile.Api.Events.External.NewClientRegistered
{
    public class NewClientRegisteredEventHandler : INotificationHandler<NewClientRegisteredEvent>
    {
        private readonly SalesContext _context;

        public NewClientRegisteredEventHandler(SalesContext context)
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