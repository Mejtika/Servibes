using System;
using FluentAssertions;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Appointments.Events;
using Servibes.Appointments.Core.Appointments.Exceptions;
using Servibes.Appointments.Core.Shared;
using Xunit;

namespace Servibes.Appointments.UnitTests
{
    public class AppointmentTests
    {
        [Fact]
        public void AppointmentWithCorrectDataShouldBeCreated()
        {
            var appointmentId = Guid.NewGuid();
            var reserveeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var employee = Employee.Create(Guid.NewGuid(), "TestEmployee");
            var service = Service.Create(123, "TestService");
            var now = DateTime.Today.AddHours(11);
            var reservationDate = ReservationDate.Create(DateTime.Today.AddHours(12), DateTime.Today.AddHours(14), now);
            var appointment = Appointment.Create(appointmentId, reserveeId, companyId, employee, service, reservationDate);

            appointment.DomainEvents.Should().ContainSingle();
            appointment.DomainEvents.Should().AllBeOfType<AppointmentStateChanged>();
        }

        [Fact]
        public void NotConfirmedAppointmentCanBeChangedToConfirmed()
        {
            var appointment = CreateAppointment();

            appointment.Confirm();

            appointment.DomainEvents.Should().ContainSingle();
            appointment.DomainEvents.Should().AllBeOfType<AppointmentStateChanged>();
        }

        [Fact]
        public void CanceledAppointmentCannotBeConfirmed()
        {
            var appointment = CreateCanceledAppointment();

            appointment.Invoking(appointment => appointment.Confirm())
                .Should().Throw<CannotChangeAppointmentStateException>()
                .WithMessage($"Cannot change state for appointment {appointment.AppointmentId} from {AppointmentStatus.Canceled} to {AppointmentStatus.Confirmed}");
        }

        [Fact]
        public void AppointmentWithPassedDateCannotBeCanceled()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(14).AddMinutes(1);
            appointment.Invoking(appointment => appointment.Cancel(now, "canceled"))
                .Should().Throw<CannotCancelFinishedAppointmentException>();
        }

        [Fact]
        public void ConfirmedAppointmentCanBeCanceled()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(11);
            appointment.Cancel(now, "canceled");

            appointment.DomainEvents.Should().ContainSingle();
            appointment.DomainEvents.Should().AllBeOfType<AppointmentStateChanged>();
        }

        [Fact]
        public void NotConfirmedAppointmentCannotBeCanceled()
        {
            var appointment = CreateAppointment();

            var now = DateTime.Today.AddHours(14).AddMinutes(1);
            appointment.Invoking(appointment => appointment.Cancel(now, "canceled"))
                .Should().Throw<CannotChangeAppointmentStateException>()
                .WithMessage($"Cannot change state for appointment {appointment.AppointmentId} from {AppointmentStatus.NotConfirmed} to {AppointmentStatus.Canceled}");
        }
        
        [Fact]
        public void AlreadyStartedAppointmentCannotBeCanceled()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(13);
            appointment.Invoking(appointment => appointment.Cancel(now, "canceled"))
                .Should().Throw<CannotCancelStartedAppointmentException>()
                .WithMessage($"Cannot cancel appointment {appointment.AppointmentId}, it is already started.");
        }

        [Fact]
        public void ConfirmedAppointmentCanBeMarkAsNoShow()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(13);
            appointment.MarkAsNoShow(now);

            appointment.DomainEvents.Should().ContainSingle();
            appointment.DomainEvents.Should().AllBeOfType<AppointmentStateChanged>();
        }

        [Fact]
        public void NotConfirmedAppointmentCannotBeMarkAsNoShow()
        {
            var appointment = CreateAppointment();

            var now = DateTime.Today.AddHours(13);
            appointment.Invoking(appointment => appointment.MarkAsNoShow(now))
                .Should().Throw<CannotChangeAppointmentStateException>()
                .WithMessage($"Cannot change state for appointment {appointment.AppointmentId} from {AppointmentStatus.NotConfirmed} to {AppointmentStatus.NoShow}");
        }

        [Fact]
        public void NotStartedAppointmentCannotBeMarkAsNoShow()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(11);
            appointment.Invoking(appointment => appointment.MarkAsNoShow(now))
                .Should().Throw<AppointmentNotStartedException>()
                .WithMessage($"Appointment {appointment.AppointmentId} is not started yet.");
        }

        [Fact]
        public void NotConfirmedAppointmentCannotBeFinished()
        {
            var appointment = CreateAppointment();

            var now = DateTime.Today.AddHours(15);
            appointment.Invoking(appointment => appointment.Finish(now))
                .Should().Throw<CannotChangeAppointmentStateException>()
                .WithMessage($"Cannot change state for appointment {appointment.AppointmentId} from {AppointmentStatus.NotConfirmed} to {AppointmentStatus.Finished}");
        }

        [Fact]
        public void AppointmentWithNotPassedDateCannotBeFinished()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(13);
            appointment.Invoking(appointment => appointment.Finish(now))
                .Should().Throw<AppointmentDateIsNotPassedException>()
                .WithMessage($"Appointment {appointment.AppointmentId} date is not passed.");
        }

        [Fact]
        public void AppointmentWithPassedDateCanBeFinished()
        {
            var appointment = CreateConfirmedAppointment();

            var now = DateTime.Today.AddHours(14).AddMinutes(1);
            appointment.Finish(now);


        }

        private static Appointment CreateAppointment()
        {
            var appointmentId = Guid.NewGuid();
            var reserveeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var employee = Employee.Create(Guid.NewGuid(), "TestEmployee");
            var service = Service.Create(123, "TestService");
            var now = DateTime.Today.AddHours(11);
            var reservationDate = ReservationDate.Create(DateTime.Today.AddHours(12), DateTime.Today.AddHours(14), now);
            var appointment = Appointment.Create(appointmentId, reserveeId, companyId, employee, service, reservationDate);
            appointment.ClearDomainEvents();
            return appointment;
        }

        private static Appointment CreateCanceledAppointment()
        {
            var appointment = CreateAppointment();
            appointment.Confirm();
            var now = DateTime.Today.AddHours(11);
            appointment.Cancel(now, "canceled");
            appointment.ClearDomainEvents();
            return appointment;
        }

        private static Appointment CreateConfirmedAppointment()
        {
            var appointment = CreateAppointment();
            appointment.Confirm();
            appointment.ClearDomainEvents();
            return appointment;
        }
    }
}
