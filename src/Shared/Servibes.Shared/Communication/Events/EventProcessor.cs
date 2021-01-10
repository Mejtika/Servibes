using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Servibes.Shared.BuildingBlocks;
using Servibes.Shared.Communication.Brokers;

namespace Servibes.Shared.Communication.Events
{
    internal sealed class EventProcessor : IEventProcessor
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapperCompositionRoot _eventMapperCompositionRoot;

        public EventProcessor(IMessageBroker broker, IEventMapperCompositionRoot eventMapperCompositionRoot)
        {
            _messageBroker = broker;
            _eventMapperCompositionRoot = eventMapperCompositionRoot;
        }

        public async Task ProcessAsync(IEnumerable<IDomainEvent> events)
        {
            var integrationEvents = MapEvents(events);
            foreach (var @event in integrationEvents)
            {
                await _messageBroker.PublishAsync(@event);
            }
        }

        private IEnumerable<INotification> MapEvents(IEnumerable<IDomainEvent> events)
        {
            if (events == null)
            {
                throw new Exception("Events list is empty");
            }

            var integrationEvents = new List<INotification>();
            foreach (var @event in events)
            {
                var integrationEvent = _eventMapperCompositionRoot.Map(@event);
                if (integrationEvent == null)
                {
                    continue;
                }

                integrationEvents.Add(integrationEvent);
            }

            return integrationEvents;
        }
    }
}