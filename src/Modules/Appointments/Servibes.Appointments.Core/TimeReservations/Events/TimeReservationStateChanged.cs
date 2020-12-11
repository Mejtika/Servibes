using System;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservations
{
    public class TimeReservationStateChanged : IDomainEvent
    {
        public Guid TimeReservationId { get; }

        public TimeReservationStatus Status { get; }

        public TimeReservationStateChanged(Guid timeReservationId, TimeReservationStatus status)
        {
            TimeReservationId = timeReservationId;
            Status = status;
        }
    }
}