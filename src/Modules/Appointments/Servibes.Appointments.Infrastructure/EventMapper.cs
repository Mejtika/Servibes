using System.Collections.Generic;
using System.Linq;
using MediatR;
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
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.Confirmed => AppointmentCreatedEvent(@event),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.Canceled => AppointmentCanceledEvent(@event),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.NoShow => AppointmentCanceledEvent(@event),
                AppointmentStateChanged @event when @event.Status == AppointmentStatus.Finished => AppointmentFinishedEvent(@event),
                TimeReservationStateChanged @event when @event.Status == TimeReservationStatus.Created => new TimeReservationCreatedEvent(@event.TimeReservationId, @event.CompanyId, @event.EmployeeId),
                TimeReservationStateChanged @event when @event.Status == TimeReservationStatus.Canceled => new TimeReservationCanceledEvent(@event.TimeReservationId, @event.CompanyId, @event.EmployeeId, @event.Date.Start),
                TimeReservationStateChanged @event when @event.Status == TimeReservationStatus.Finished => new TimeReservationFinishedEvent(@event.TimeReservationId, @event.CompanyId, @event.EmployeeId, @event.Date.Start),
                _ => null
            };

        public IEnumerable<INotification> MapAll(IEnumerable<IDomainEvent> domainEvents)
            => domainEvents.Select(Map);

        private AppointmentCreatedEvent AppointmentCreatedEvent(AppointmentStateChanged @event)
        {
            return new AppointmentCreatedEvent(
                @event.AppointmentId,
                @event.ReserveeId,
                @event.CompanyId,
                @event.Employee.EmployeeId,
                @event.ReservedDate.Start,
                @event.ReservedDate.End);
        }

        private AppointmentCanceledEvent AppointmentCanceledEvent(AppointmentStateChanged @event)
        {
            return new AppointmentCanceledEvent(
                @event.AppointmentId,
                @event.ReserveeId,
                @event.CompanyId,
                @event.Employee.EmployeeId,
                @event.ReservedDate.Start,
                @event.ReservedDate.End,
                @event.CancellationReason);
        }

        private AppointmentFinishedEvent AppointmentFinishedEvent(AppointmentStateChanged @event)
        {
            return new AppointmentFinishedEvent(
                @event.AppointmentId,
                @event.ReserveeId,
                @event.CompanyId,
                @event.Employee.EmployeeId,
                @event.ReservedDate.Start,
                @event.ReservedDate.End,
                @event.Service.Name,
                @event.Service.Price);
        }
    }
}
