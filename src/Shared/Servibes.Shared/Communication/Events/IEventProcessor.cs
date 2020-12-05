using System.Collections.Generic;
using System.Threading.Tasks;
using Servibes.Shared.BuildingBlocks;


namespace Servibes.Shared.Communication
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}