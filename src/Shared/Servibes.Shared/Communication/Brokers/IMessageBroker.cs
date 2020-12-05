using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.Shared.Communication.Brokers
{
    public interface IMessageBroker
    {
        Task PublishAsync(INotification @event);
        Task PublishAsync(IEnumerable<INotification> events);
    }
}