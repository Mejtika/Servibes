using System.Collections.Generic;
using System.Threading.Tasks;


namespace Servibes.Shared.Communication
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}