using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservations.Events
{
    public class TimeReservationCreated : IDomainEvent
    {
        public TimeReservation TimeReservation { get; }

        public TimeReservationCreated(TimeReservation timeReservation)
        {
            TimeReservation = timeReservation;
        }
    }
}