using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Servibes.Shared.Communication
{
    internal sealed class EventMapperCompositionRoot : IEventMapperCompositionRoot
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventMapperCompositionRoot(IServiceScopeFactory serviceScopeFactory)
            => _serviceScopeFactory = serviceScopeFactory;

        public INotification Map(IDomainEvent domainEvent)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mappers = scope.ServiceProvider.GetService<IEnumerable<IEventMapper>>();

            return mappers
                ?.Select(mapper => mapper.Map(domainEvent))
                .SingleOrDefault(@event => @event is { });
        }

        public IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents)
            => domainEvents.Select(Map);
    }
}