using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservations.Events
{
    public class TimeReservationCanceled : IDomainEvent
    {
        public TimeReservation TimeReservation { get; }

        public TimeReservationCanceled(TimeReservation timeReservation)
        {
            TimeReservation = timeReservation;
        }
    }
}