using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Servibes.Sales.Api.Events.External.ClientInformationUpdated
{
    public class ClientInformationUpdatedEventHandler : INotificationHandler<ClientInformationUpdatedEvent>
    {
        private readonly SalesContext _context;

        public ClientInformationUpdatedEventHandler(SalesContext context)
        {
            _context = context;
        }

        public async Task Handle(ClientInformationUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == notification.ClientId, cancellationToken);
            client.FirstName = notification.FirstName;
            client.LastName = notification.LastName;
            client.Email = notification.Email;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}