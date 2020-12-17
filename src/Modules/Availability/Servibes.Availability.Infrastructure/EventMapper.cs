using System.Collections.Generic;
using System.Linq;
using MediatR;
using Servibes.Availability.Application.Events.ReservationAdded;
using Servibes.Availability.Application.Events.Reservations;
using Servibes.Availability.Core.Employees;
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
                EmployeeReservationAddedDomainEvent @event when @event.ReservationSnapshot == null 
                    => new TimeReservationAddedEvent(
                        @event.EmployeeId,
                        @event.CompanyId,
                        @event.Reservation.Start,
                        @event.Reservation.End),
                EmployeeReservationAddedDomainEvent @event => ReservationAddedEvent(@event.ReservationSnapshot),
                _ => null
            };

        public IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents)
            => domainEvents.Select(Map);

        private static ReservationAddedEvent ReservationAddedEvent(ReservationSnapshot snapshot)
        {
            return new ReservationAddedEvent(snapshot.CompanyId,
                snapshot.EmployeeId,
                snapshot.ReserveeId,
                snapshot.EmployeeName,
                snapshot.ServiceName,
                snapshot.ServicePrice,
                snapshot.Start,
                snapshot.End);
        }
    }
}
