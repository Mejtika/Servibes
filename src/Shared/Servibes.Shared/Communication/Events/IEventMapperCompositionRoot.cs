using System.Collections.Generic;
using MediatR;

namespace Servibes.Shared.Communication
{
    public interface IEventMapperCompositionRoot
    {
        INotification Map(IDomainEvent domainEvent);
        IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents);
    }
}