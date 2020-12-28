using System;
using Servibes.Appointments.Core.Appointments.Events;
using Servibes.Appointments.Core.Appointments.Exceptions;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments
{
    public class Appointment : Entity, IAggregateRoot
    {
        public Guid AppointmentId { get; private set; }

        private readonly Guid _reserveeId;

        public Guid ReserveeId => _reserveeId;

        private readonly Guid _companyId;

        public Guid CompanyId => _companyId;

        private readonly Employee _employee;

        private readonly Service _service;

        private AppointmentStatus _status;

        public AppointmentStatus AppointmentStatus => _status;

        private readonly ReservationDate _reservedDate;

        public ReservationDate ReservationDate => _reservedDate;

        private string _cancellationReason;

        private Appointment()
        {

        }

        private Appointment(Guid appointmentId, Guid reserveeId, Guid companyId, Employee employee,
            Service service, AppointmentStatus status, ReservationDate reservedDate)
        {
            AppointmentId = appointmentId;
            _reserveeId = reserveeId; 
            _companyId = companyId;
            _employee = employee;
            _service = service;
            _status = status;
            _reservedDate = reservedDate;
            _cancellationReason = string.Empty;
        }

        public static Appointment Create(Guid appointmentId, Guid reserveeId, Guid companyId, Employee employee,
            Service service, ReservationDate reservedDate)
        {
            var appointment = new Appointment(appointmentId, reserveeId, companyId, employee, service,
                AppointmentStatus.Confirmed, reservedDate);
            appointment.AddDomainEvent(new AppointmentStateChanged(
                    appointmentId,
                    reserveeId, 
                    companyId, 
                    employee, 
                    reservedDate,
                    AppointmentStatus.Confirmed,
                    string.Empty,
                    service.Price));

            return appointment;
        }

        public void Cancel(DateTime now, string reason)
        {
            if (_status != AppointmentStatus.Confirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, _status, AppointmentStatus.Canceled);
            }

            if (_reservedDate.IsInReservationTime(now))
            {
                throw new CannotCancelStartedAppointmentException(AppointmentId);
            }

            if (_reservedDate.HasPassed(now))
            {
                throw new CannotCancelFinishedAppointmentException(AppointmentId);
            }

            _status = AppointmentStatus.Canceled;
            _cancellationReason = reason ?? string.Empty;
            AddDomainEvent(AppointmentStateChanged());
        }

        public void MarkAsNoShow(DateTime now)
        {
            if (_status != AppointmentStatus.Confirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, _status, AppointmentStatus.NoShow);
            }

            if (!_reservedDate.IsInReservationTime(now))
            {
                throw new AppointmentNotStartedException(AppointmentId);
            }

            _status = AppointmentStatus.NoShow;
            _cancellationReason = "The customer didn't come";
            AddDomainEvent(AppointmentStateChanged());
        }

        public void Finish(DateTime now)
        {
            if (_status != AppointmentStatus.Confirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, _status, AppointmentStatus.Finished);
            }

            if (!_reservedDate.HasPassed(now))
            {
                throw new AppointmentDateIsNotPassedException(AppointmentId);
            }

            _status = AppointmentStatus.Finished;
            AddDomainEvent(AppointmentStateChanged());
        }

        private AppointmentStateChanged AppointmentStateChanged()
        {
            return new AppointmentStateChanged(
                AppointmentId,
                _reserveeId,
                _companyId,
                _employee,
                _reservedDate,
                _status,
                _cancellationReason,
                _service.Price);
        }
    }
}
