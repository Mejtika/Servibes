using System;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservation
{
    public class TimeReservation : Entity, IAggregateRoot
    {
        public Guid TimeReservationId { get; private set; }

        private readonly Guid _companyId;

        private readonly Guid _employeeId;

        private readonly ReservationDate _reservedDate;

        private bool _isCanceled;

        private TimeReservation(Guid timeReservationId, Guid companyId, Guid employeeId, ReservationDate reservedDate)
        {
            TimeReservationId = timeReservationId;
            _companyId = companyId;
            _employeeId = employeeId;
            _reservedDate = reservedDate;
            _isCanceled = false;
        }

        public static TimeReservation Create(Guid timeReservationId, Guid companyId, Guid employeeId, ReservationDate reservedDate)
        {
            var timeReservation = new TimeReservation(timeReservationId, companyId, employeeId, reservedDate);
            timeReservation.AddDomainEvent(new TimeReservationCreated(timeReservation));
            return timeReservation;
        }

        public void Cancel()
        {
            if (_isCanceled)
            {
                throw new TimeReservationIsAlreadyCanceledException(TimeReservationId);
            }
            _isCanceled = true;
            AddDomainEvent(new TimeReservationCanceled(this));
        }
    }
}
