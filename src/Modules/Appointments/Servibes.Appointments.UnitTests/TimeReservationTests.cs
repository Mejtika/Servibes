using System;
using FluentAssertions;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Appointments.Core.TimeReservations.Exceptions;
using Xunit;

namespace Servibes.Appointments.UnitTests
{
    public class TimeReservationTests
    {
        [Fact]
        public void TimeReservationWithCorrectDataShouldBeCreated()
        {
            var timeReservationId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var reservationDate = ReservationDate.Create(
                DateTime.Today.AddHours(12),
                DateTime.Today.AddHours(14),
                DateTime.Today.AddHours(10));
            var timeReservation = TimeReservation.Create(timeReservationId, companyId, employeeId, reservationDate);

            timeReservation.DomainEvents.Should().ContainSingle();
            timeReservation.DomainEvents.Should().AllBeOfType<TimeReservationStateChangedDomainEvent>();
        }

        [Fact]
        public void TimeReservationCanBeCanceled()
        {
            var timeReservation = CreateTimeReservation();
            var now = DateTime.Today.AddHours(11);

            timeReservation.Cancel(now);

            timeReservation.DomainEvents.Should().ContainSingle();
            timeReservation.DomainEvents.Should().AllBeOfType<TimeReservationStateChangedDomainEvent>();
        }

        [Fact]
        public void TimeReservationWithPassedDateCannotBeCanceled()
        {
            var timeReservation = CreateCanceledTimeReservation();
            var now = DateTime.Today.AddHours(14).AddMinutes(1);

            timeReservation.Invoking(timeReservation => timeReservation.Cancel(now))
                .Should().Throw<CannotCancelFinishedTimeReservationException>()
                .WithMessage($"Finished time reservation {timeReservation.TimeReservationId} cannot be canceled.");
        }

        private static TimeReservation CreateTimeReservation()
        {
            var timeReservationId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var reservationDate = ReservationDate.Create(
                DateTime.Today.AddHours(12),
                DateTime.Today.AddHours(14),
                DateTime.Today.AddHours(10));
            var timeReservation = TimeReservation.Create(timeReservationId, companyId, employeeId, reservationDate);
            timeReservation.ClearDomainEvents();
            return timeReservation;
        }

        private static TimeReservation CreateCanceledTimeReservation()
        {
            var timeReservation = CreateTimeReservation();
            var now = DateTime.Today.AddHours(10);
            timeReservation.Cancel(now);
            timeReservation.ClearDomainEvents();
            return timeReservation;
        }
    }
}
