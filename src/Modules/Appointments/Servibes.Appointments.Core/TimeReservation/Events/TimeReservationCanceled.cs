using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservation
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