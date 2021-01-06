using System;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservations.Exceptions;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservations
{
    public class TimeReservation : Entity, IAggregateRoot
    {
        public Guid TimeReservationId { get; private set; }

        private readonly Guid _companyId;

        public Guid CompanyId => _companyId;

        private readonly Guid _employeeId;

        private readonly ReservationDate _reservedDate;

        public ReservationDate ReservationDate => _reservedDate;

        private TimeReservationStatus _status;

        public TimeReservationStatus Status => _status;

        private TimeReservation()
        {

        }

        private TimeReservation(Guid timeReservationId, Guid companyId, Guid employeeId, ReservationDate reservedDate, TimeReservationStatus status)
        {
            TimeReservationId = timeReservationId;
            _companyId = companyId;
            _employeeId = employeeId;
            _reservedDate = reservedDate;
            _status = status;
        }

        public static TimeReservation Create(Guid timeReservationId, Guid companyId, Guid employeeId, ReservationDate reservedDate)
        {
            var timeReservation = new TimeReservation(timeReservationId, companyId, employeeId, reservedDate, TimeReservationStatus.Created);
            timeReservation.AddDomainEvent(new TimeReservationStateChanged(timeReservationId, companyId, employeeId, reservedDate, timeReservation._status));
            return timeReservation;
        }

        public void Cancel(DateTime now)
        {
            if (_reservedDate.HasPassed(now))
            {
                throw new CannotCancelFinishedTimeReservationException(TimeReservationId);
            }

            if (_status != TimeReservationStatus.Created)
            {
                throw new CannotChangeTimeReservationStateException(TimeReservationId, _status, TimeReservationStatus.Canceled);
            }
            _status = TimeReservationStatus.Canceled;
            AddDomainEvent(new TimeReservationStateChanged(TimeReservationId, _companyId, _employeeId, _reservedDate, _status));
        }

        public void Finish(DateTime now)
        {
            if (!_reservedDate.HasPassed(now))
            {
                throw new TimeReservationDateIsNotPassedException(TimeReservationId);
            }

            if (_status != TimeReservationStatus.Created)
            {
                throw new CannotChangeTimeReservationStateException(TimeReservationId, _status, TimeReservationStatus.Canceled);
            }

            _status = TimeReservationStatus.Finished;
            AddDomainEvent(new TimeReservationStateChanged(TimeReservationId, _companyId, _employeeId, _reservedDate, _status));
        }
    }
}
