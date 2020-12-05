using System.Collections.Generic;
using MediatR;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Shared.Communication.Events
{
    public interface IEventMapper
    {
        INotification Map(IDomainEvent domainEvent);
        IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents);
    }
}