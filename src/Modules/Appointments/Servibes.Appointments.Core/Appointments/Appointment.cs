using System;
using Servibes.Appointments.Core.Appointments.Events;
using Servibes.Appointments.Core.Appointments.Exceptions;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments
{
    public class Appointment : Entity, IAggregateRoot
    {
        private string _cancellationReason;

        public Guid AppointmentId { get; private set; }

        public Guid ReserveeId { get; private set; }

        public Guid CompanyId { get; private set; }

        public Employee Employee { get; private set; }

        public Service Service { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public ReservationDate ReservedDate { get; private set; }

        private Appointment()
        {

        }

        private Appointment(Guid appointmentId, Guid reserveeId, Guid companyId, Employee employee,
            Service service, AppointmentStatus status, ReservationDate reservedDate)
        {
            AppointmentId = appointmentId;
            ReserveeId = reserveeId; 
            CompanyId = companyId;
            Employee = employee;
            Service = service;
            Status = status;
            ReservedDate = reservedDate;
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
                    service));

            return appointment;
        }

        public void Cancel(DateTime now, string reason)
        {
            if (Status != AppointmentStatus.Confirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, Status, AppointmentStatus.Canceled);
            }

            if (ReservedDate.IsInReservationTime(now))
            {
                throw new CannotCancelStartedAppointmentException(AppointmentId);
            }

            if (ReservedDate.HasPassed(now))
            {
                throw new CannotCancelFinishedAppointmentException(AppointmentId);
            }

            Status = AppointmentStatus.Canceled;
            _cancellationReason = reason ?? string.Empty;
            AddDomainEvent(AppointmentStateChanged());
        }

        public void Finish(DateTime now)
        {
            if (Status != AppointmentStatus.Confirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, Status, AppointmentStatus.Finished);
            }

            if (!ReservedDate.HasPassed(now))
            {
                throw new AppointmentDateIsNotPassedException(AppointmentId);
            }

            Status = AppointmentStatus.Finished;
            AddDomainEvent(AppointmentStateChanged());
        }

        public void MarkAsNoShow(DateTime now)
        {
            if (Status != AppointmentStatus.Confirmed)
            {
                throw new CannotChangeAppointmentStateException(AppointmentId, Status, AppointmentStatus.NoShow);
            }

            if (!ReservedDate.IsInReservationTime(now))
            {
                throw new AppointmentNotStartedException(AppointmentId);
            }

            Status = AppointmentStatus.NoShow;
            _cancellationReason = "The customer didn't come";
            AddDomainEvent(AppointmentStateChanged());
        }

        private AppointmentStateChanged AppointmentStateChanged()
        {
            return new AppointmentStateChanged(
                AppointmentId,
                ReserveeId,
                CompanyId,
                Employee,
                ReservedDate,
                Status,
                _cancellationReason,
                Service);
        }
    }
}
