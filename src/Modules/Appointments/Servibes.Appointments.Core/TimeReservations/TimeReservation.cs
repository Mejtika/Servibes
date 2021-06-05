using System;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservations.Exceptions;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.TimeReservations
{
    public class TimeReservation : Entity, IAggregateRoot
    {
        public Guid TimeReservationId { get; private set; }

        public Guid CompanyId { get; private set; }

        public Guid EmployeeId { get; private set; }

        public  ReservationDate ReservedDate { get; private set; }

        public TimeReservationStatus Status { get; private set; }

        private TimeReservation()
        {

        }

        private TimeReservation(Guid timeReservationId, Guid companyId, Guid employeeId, ReservationDate reservedDate, TimeReservationStatus status)
        {
            TimeReservationId = timeReservationId;
            CompanyId = companyId;
            EmployeeId = employeeId;
            ReservedDate = reservedDate;
            Status = status;
        }

        public static TimeReservation Create(Guid timeReservationId, Guid companyId, Guid employeeId, ReservationDate reservedDate)
        {
            var timeReservation = new TimeReservation(timeReservationId, companyId, employeeId, reservedDate, TimeReservationStatus.Created);
            timeReservation.AddDomainEvent(new TimeReservationStateChanged(timeReservationId, companyId, employeeId, reservedDate, timeReservation.Status));
            return timeReservation;
        }

        public void Cancel(DateTime now)
        {
            if (ReservedDate.HasPassed(now))
            {
                throw new CannotCancelFinishedTimeReservationException(TimeReservationId);
            }

            if (Status != TimeReservationStatus.Created)
            {
                throw new CannotChangeTimeReservationStateException(TimeReservationId, Status, TimeReservationStatus.Canceled);
            }
            Status = TimeReservationStatus.Canceled;
            AddDomainEvent(new TimeReservationStateChanged(TimeReservationId, CompanyId, EmployeeId, ReservedDate, Status));
        }

        public void Finish(DateTime now)
        {
            if (Status != TimeReservationStatus.Created)
            {
                throw new CannotChangeTimeReservationStateException(TimeReservationId, Status, TimeReservationStatus.Canceled);
            }

            if (!ReservedDate.HasPassed(now))
            {
                throw new TimeReservationDateIsNotPassedException(TimeReservationId);
            }

            Status = TimeReservationStatus.Finished;
            AddDomainEvent(new TimeReservationStateChanged(TimeReservationId, CompanyId, EmployeeId, ReservedDate, Status));
        }
    }
}
