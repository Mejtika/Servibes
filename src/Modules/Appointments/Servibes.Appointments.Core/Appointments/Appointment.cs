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

        private readonly Guid _companyId;

        private readonly Employee _employee;

        private readonly Service _service;

        private AppointmentStatus _status;

        private readonly ReservationDate _reservedDate;

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
                AppointmentStatus.NotConfirmed, reservedDate);
            appointment.AddDomainEvent(new AppointmentStateChanged(appointment.AppointmentId,
                appointment._employee.EmployeeId, appointment._companyId, reservedDate.Start, reservedDate.End, appointment._status));
            return appointment;
        }

        public void Confirm()
        {
            if (_status != AppointmentStatus.NotConfirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, _status, AppointmentStatus.Confirmed);
            }

            _status = AppointmentStatus.Confirmed;
            //AddDomainEvent(new AppointmentStateChanged(AppointmentId, _employee.EmployeeId, _companyId, _status));
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
            //AddDomainEvent(new AppointmentStateChanged(AppointmentId, _employee.EmployeeId, _companyId, _status));
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
            //AddDomainEvent(new AppointmentStateChanged(AppointmentId, _employee.EmployeeId, _companyId, _status));
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
            //AddDomainEvent(new AppointmentStateChanged(AppointmentId, _employee.EmployeeId, _companyId, _status));
        }
    }
}
