using System.Collections.Generic;
using System.Linq;
using MediatR;
using Servibes.Appointments.Application.Events;
using Servibes.Appointments.Application.Events.Appointments;
using Servibes.Appointments.Application.Events.TimeReservations;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Appointments.Events;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Shared.BuildingBlocks;
using Servibes.Shared.Communication.Events;

namespace Servibes.Appointments.Infrastructure
{
    public class EventMapper : IEventMapper
    {
        public INotification Map(IDomainEvent domainEvent)
            => domainEvent switch
            {
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.NotConfirmed => new AppointmentCreatedEvent(@event.AppointmentId),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.Confirmed => new AppointmentConfirmedEvent(@event.AppointmentId),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.Canceled => new AppointmentCanceledEvent(@event.AppointmentId),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.NoShow => new AppointmentCanceledEvent(@event.AppointmentId),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.Finished => new AppointmentFinishedEvent(@event.AppointmentId),
                TimeReservationStateChanged @event when @event.Status == TimeReservationStatus.Created => new TimeReservationCreatedEvent(@event.TimeReservationId),
                TimeReservationStateChanged @event when @event.Status == TimeReservationStatus.Canceled => new TimeReservationCanceledEvent(@event.TimeReservationId),
                TimeReservationStateChanged @event when @event.Status == TimeReservationStatus.Finished => new TimeReservationFinishedEvent(@event.TimeReservationId),
                _ => null
            };

        public IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents)
            => domainEvents.Select(Map);
    }
}
