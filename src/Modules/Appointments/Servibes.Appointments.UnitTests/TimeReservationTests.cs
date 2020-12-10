using System;
using FluentAssertions;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Appointments.Core.TimeReservations.Events;
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
            timeReservation.DomainEvents.Should().AllBeOfType<TimeReservationCreated>();
        }

        [Fact]
        public void TimeReservationCanBeCanceled()
        {
            var timeReservation = CreateTimeReservation();

            timeReservation.Cancel();

            timeReservation.DomainEvents.Should().ContainSingle();
            timeReservation.DomainEvents.Should().AllBeOfType<TimeReservationCanceled>();
        }

        [Fact]
        public void CanceledTimeReservationCannotBeCanceledOnceMore()
        {
            var timeReservation = CreateCanceledTimeReservation();

            timeReservation.Invoking(timeReservation => timeReservation.Cancel())
                .Should().Throw<TimeReservationIsAlreadyCanceledException>()
                .WithMessage($"Cannot cancel already canceled time reservation {timeReservation.TimeReservationId}.");
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
            timeReservation.Cancel();
            timeReservation.ClearDomainEvents();
            return timeReservation;
        }
    }
}
