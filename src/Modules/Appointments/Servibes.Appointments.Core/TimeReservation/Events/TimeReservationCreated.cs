using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservation
{
    public class TimeReservationCreated : IDomainEvent
    {
        public TimeReservationCreated(TimeReservation timeReservation)
        {
        }
    }
}