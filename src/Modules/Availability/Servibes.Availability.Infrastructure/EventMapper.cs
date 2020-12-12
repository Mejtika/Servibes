using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Servibes.Availability.Application.Events.ReservationAdded;
using Servibes.Availability.Core.Employees.Events;
using Servibes.Shared.BuildingBlocks;
using Servibes.Shared.Communication.Events;

namespace Servibes.Availability.Infrastructure
{
    public class EventMapper : IEventMapper
    {
        public INotification Map(IDomainEvent domainEvent)
            => domainEvent switch
            {
                EmployeeReservationAddedDomainEvent @event => new ReservationAddedEvent(@event.Employee.EmployeeId, @event.Reservation.Start),
                _ => null
            };

        public IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents)
            => domainEvents.Select(Map);
    }
}
