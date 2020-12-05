using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.Shared.Communication.Brokers
{
    internal sealed class MessageBroker : IMessageBroker
    {
        private readonly IModuleClient _client;

        public MessageBroker(IModuleClient client)
        {
            _client = client;
        }

        public async Task PublishAsync(INotification @event) => await _client.PublishAsync(@event);

        public async Task PublishAsync(IEnumerable<INotification> events)
        {
            var tasks = events.Select(PublishAsync).ToArray();
            await Task.WhenAll(tasks);
        }
    }
}