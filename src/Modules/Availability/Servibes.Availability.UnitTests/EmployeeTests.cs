using System;
using System.Collections.Generic;
using FluentAssertions;
using Servibes.Availability.Core;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Core.Employees.Events;
using Servibes.Availability.Core.Employees.Exceptions;
using Servibes.Availability.Core.Shared;
using Xunit;

namespace Servibes.Availability.UnitTests
{
    public class EmployeeTests
    {
        public static List<object[]> Reservations()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2020, 12, 11, 14, 0, 0), new DateTime(2020, 12, 11, 15, 30, 0) },
                new object[] { new DateTime(2020, 12, 11, 13, 50, 0), new DateTime(2020, 12, 11, 15, 55, 0) },
                new object[] { new DateTime(2020, 12, 11, 17, 20, 0), new DateTime(2020, 12, 11, 17, 50, 0) },
            };
        }

        public static List<object[]> CollidingReservations()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2020, 12, 11, 13, 0, 0), new DateTime(2020, 12, 11, 14, 30, 0) },
                new object[] { new DateTime(2020, 12, 11, 13, 40, 0), new DateTime(2020, 12, 11, 16, 30, 0) },
                new object[] { new DateTime(2020, 12, 11, 17, 0, 0), new DateTime(2020, 12, 11, 17, 30, 0) },
            };
        }

        public static List<object[]> ReservationsOutOfWorkingHours()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2020, 12, 11, 6, 0, 0), new DateTime(2020, 12, 11, 8, 30, 0) },
                new object[] { new DateTime(2020, 12, 11, 9, 45, 0), new DateTime(2020, 12, 11, 11, 00, 0) },
                new object[] { new DateTime(2020, 12, 11, 17, 55, 0), new DateTime(2020, 12, 11, 18, 40, 0) },
            };
        }

        public static List<object[]> TimeOffs()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2020, 12, 12, 14, 0, 0), new DateTime(2020, 12, 24, 14, 30, 0) },
                new object[] { new DateTime(2020, 12, 7, 14, 15, 0), new DateTime(2020, 12, 9, 15, 45, 0) },
                new object[] { new DateTime(2020, 12, 15, 17, 25, 0), new DateTime(2020, 12, 25, 17, 55, 0) },
            };
        }

        public static List<object[]> CollidingTimeOffs()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2020, 12, 8, 14, 0, 0), new DateTime(2020, 12, 24, 14, 30, 0) },
                new object[] { new DateTime(2020, 12, 10, 14, 15, 0), new DateTime(2021, 1, 20, 15, 45, 0) },
                new object[] { new DateTime(2020, 12, 11, 17, 25, 0), new DateTime(2020, 12, 25, 17, 55, 0) },
            };
        }

        public static List<object[]> NewWorkingHours()
        {
            return new List<object[]>
            {
                new object[] { true, TimeSpan.FromHours(12), TimeSpan.FromHours(18) },
                new object[] { false, TimeSpan.FromHours(8), TimeSpan.FromHours(22) },
                new object[] { true, TimeSpan.FromHours(14), TimeSpan.FromHours(15) },
            };
        }

        public static List<object[]> OutOfRangeWorkingHours()
        {
            return new List<object[]>
            {
                new object[] { true, TimeSpan.FromHours(7), TimeSpan.FromHours(12) },
                new object[] { true, TimeSpan.FromHours(8), TimeSpan.FromHours(22) },
                new object[] { true, TimeSpan.FromHours(18), TimeSpan.FromHours(24) },
            };
        }

        public static List<object[]> WorkingHoursWithMismatchedAvailability()
        {
            return new List<object[]>
            {
                new object[] { true, TimeSpan.FromHours(13), TimeSpan.FromHours(16) },
                new object[] { true, TimeSpan.FromHours(8), TimeSpan.FromHours(22) },
                new object[] { true, TimeSpan.FromHours(14), TimeSpan.FromHours(15) },
            };
        }

        [Fact]
        public void ShouldCreateEmployee()
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var hoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var weekWorkingHours = WeekHoursRange.Create(hoursRanges);

            var employee = Employee.Create(employeeId, companyId, weekWorkingHours);

            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeAvailabilityCreatedDomainEvent>();
        }

        [Theory]
        [MemberData(nameof(NewWorkingHours))]
        public void WorkingHoursCanBeChangedIntoNewOnes(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var companyHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var companyOpeningHours = WeekHoursRange.Create(companyHoursRanges);
            var company = Company.Create(companyId, companyOpeningHours);
            var employee = Employee.Create(employeeId, companyId, companyOpeningHours);
            var newEmployeeHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(15), TimeSpan.FromHours(16)),
                HoursRange.Create(DayOfWeek.Thursday, isAvailable, start, end),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(14), TimeSpan.FromHours(15)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(16), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(10), TimeSpan.FromHours(17)),
            };
            var newEmployeeWorkingHours = WeekHoursRange.Create(newEmployeeHoursRanges);

            employee.ChangeWorkingHours(company.OpeningHours, newEmployeeWorkingHours);

            employee.WorkingHours.Should().Be(newEmployeeWorkingHours);
        }

        [Theory]
        [MemberData(nameof(OutOfRangeWorkingHours))]
        public void ChangeOfOutOfRangeWorkingHoursShouldBeRejected(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var companyHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var companyOpeningHours = WeekHoursRange.Create(companyHoursRanges);
            var company = Company.Create(companyId, companyOpeningHours);
            var employee = Employee.Create(employeeId, companyId, companyOpeningHours);
            var newEmployeeHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(15), TimeSpan.FromHours(16)),
                HoursRange.Create(DayOfWeek.Thursday, isAvailable, start, end),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(14), TimeSpan.FromHours(15)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(16), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(10), TimeSpan.FromHours(17)),
            };
            var newEmployeeWorkingHours = WeekHoursRange.Create(newEmployeeHoursRanges);

            employee.Invoking(employee => employee.ChangeWorkingHours(company.OpeningHours, newEmployeeWorkingHours))
                .Should().Throw<IncorrectWorkingHoursException>()
                .WithMessage("Employee working hours are colliding with company opening hours.");
        }

        [Theory]
        [MemberData(nameof(WorkingHoursWithMismatchedAvailability))]
        public void ChangeOfWorkingHoursWithMismatchedAvailabilityShouldBeRejected(bool isAvailable, TimeSpan start, TimeSpan end)
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var companyHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var companyOpeningHours = WeekHoursRange.Create(companyHoursRanges);
            var company = Company.Create(companyId, companyOpeningHours);
            var employee = Employee.Create(employeeId, companyId, companyOpeningHours);
            var newEmployeeHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(15), TimeSpan.FromHours(16)),
                HoursRange.Create(DayOfWeek.Thursday, isAvailable, start, end),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(14), TimeSpan.FromHours(15)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(16), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(10), TimeSpan.FromHours(17)),
            };
            var newEmployeeWorkingHours = WeekHoursRange.Create(newEmployeeHoursRanges);

            employee.Invoking(employee => employee.ChangeWorkingHours(company.OpeningHours, newEmployeeWorkingHours))
                .Should().Throw<IncorrectWorkingHoursException>()
                .WithMessage("Found mismatch between employee's and company days availability.");
        }

        [Fact]
        public void ShouldAdjustEmployeeWorkingHoursToCompaniesOpeningHours()
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var employeeHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(14), TimeSpan.FromHours(17)),
                HoursRange.Create(DayOfWeek.Wednesday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var employeeWorkingHours = WeekHoursRange.Create(employeeHoursRanges);
            var employee = Employee.Create(employeeId, companyId, employeeWorkingHours);
            var companyHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(13), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(12), TimeSpan.FromHours(18)),
            };
            var companyOpeningHours = WeekHoursRange.Create(companyHoursRanges);
            var company = Company.Create(companyId, companyOpeningHours);

            employee.AdjustWorkingHours(company.OpeningHours);

            employee.WorkingHours.Should().Be(company.OpeningHours);
        }

        [Fact]
        public void NewReservationCanBeAddedToEmployeeWithoutExistingOnes()
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var start = new DateTime(2020, 12, 11, 12, 30, 0);
            var end = new DateTime(2020, 12, 11, 14, 0, 0);
            var newReservation = Reservation.Create(start, end, now);
            var employee = CreateEmployeeWithNoReservationsAndTimeOffs();

            employee.AddReservation(newReservation);

            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeReservationAddedDomainEvent>();
        }

        [Theory]
        [MemberData(nameof(Reservations))]
        public void NewReservationCanBeAddedToEmployeeWithExistingOnes(DateTime start, DateTime end)
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var newReservation = Reservation.Create(start, end, now);
            var employee = CreateEmployeeWithReservations();

            employee.AddReservation(newReservation);

            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeReservationAddedDomainEvent>();
        }

        [Theory]
        [MemberData(nameof(CollidingReservations))]
        public void NewReservationCollidingWithExistingOnesShouldBeRejected(DateTime start, DateTime end)
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var newReservation = Reservation.Create(start, end, now);
            var employee = CreateEmployeeWithReservations();
            employee.AddReservation(newReservation);
            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeReservationCanceledDomainEvent>();
        }

        [Theory]
        [MemberData(nameof(ReservationsOutOfWorkingHours))]
        public void NewReservationOutOfWorkingHoursShouldBeRejected(DateTime start, DateTime end)
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var newReservation = Reservation.Create(start, end, now);
            var employee = CreateEmployeeWithReservations();
            employee.AddReservation(newReservation);
            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeReservationCanceledDomainEvent>();
        }

        [Theory]
        [MemberData(nameof(TimeOffs))]
        public void NewTimeOffCanBeAdded(DateTime start, DateTime end)
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var newTimeOff = TimeOff.Create(start, end, now);
            var employee = CreateEmployeeWithReservationsAndTimeOffs();
            employee.GiveTimeOff(newTimeOff);
            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeTimeOffReceivedDomainEvent>();
        }

        [Fact]
        public void ExistingTimeOffsCanBeReleased()
        {
            var employee = CreateEmployeeWithReservations();
            var timeOff = TimeOff.Create(
                new DateTime(2020, 12, 10, 12, 30, 0),
                new DateTime(2020, 12, 12, 13, 45, 0),
                new DateTime(2020, 12, 5, 14, 0, 0));
            employee.GiveTimeOff(timeOff);
            employee.ClearDomainEvents();

            employee.ReleaseTimeOff(timeOff);

            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeTimeOffReleasedDomainEvent>();
        }

        [Theory]
        [MemberData(nameof(CollidingTimeOffs))]
        public void NewTimeOffCollidingWithExistingOneShouldBeRejected(DateTime start, DateTime end)
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var newTimeOff = TimeOff.Create(start, end, now);
            var employee = CreateEmployeeWithReservationsAndTimeOffs();
            employee.Invoking(employee => employee.GiveTimeOff(newTimeOff))
                .Should().Throw<TimeOffCollidingDatesException>()
                .WithMessage($"Time off dates {start} and {end} are colliding with existing one.");
        }

        [Theory]
        [MemberData(nameof(Reservations))]
        public void NewReservationCollidingWithTimeOffShouldBeRejected(DateTime start, DateTime end)
        {
            var now = new DateTime(2020, 12, 5, 12, 30, 0);
            var newReservation = Reservation.Create(start, end, now);
            var employee = CreateEmployeeWithReservationsAndTimeOffs();
            employee.AddReservation(newReservation);
            employee.DomainEvents.Should().ContainSingle();
            employee.DomainEvents.Should().AllBeOfType<EmployeeReservationCanceledDomainEvent>();
        }

        [Fact]
        public void EmployeeWithExistingReservationsShouldBeDeleted()
        {
            var employee = CreateEmployeeWithReservations();
            employee.Delete();
            employee.DomainEvents.Count.Should().Be(3);
        }

        [Fact]
        public void ExistingReservationsCanBeReleased()
        {
            var employee = CreateEmployeeWithNoReservationsAndTimeOffs();
            var reservations = new List<Reservation>
            {
                Reservation.Create(
                    new DateTime(2020, 12, 11, 12, 30, 0),
                    new DateTime(2020, 12, 11, 13, 45, 0),
                    new DateTime(2020, 12, 5, 14, 0, 0)),
                Reservation.Create(
                    new DateTime(2020, 12, 11, 16, 0, 0),
                    new DateTime(2020, 12, 11, 17, 15, 0),
                    new DateTime(2020, 12, 5, 14, 0, 0))
            };
            reservations.ForEach(reservation => employee.AddReservation(reservation));
            employee.ClearDomainEvents();

            reservations.ForEach(reservation => employee.ReleaseReservation(reservation));
            employee.DomainEvents.Count.Should().Be(2);
            employee.DomainEvents.Should().AllBeOfType<EmployeeReservationReleasedDomainEvent>();
        }

        private static Employee CreateEmployeeWithNoReservationsAndTimeOffs()
        {
            var employeeId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var employeeHoursRanges = new List<HoursRange>
            {
                HoursRange.Create(DayOfWeek.Monday, true, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Tuesday, true, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Wednesday, true, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Thursday, true, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Friday, true, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Saturday, false, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
                HoursRange.Create(DayOfWeek.Sunday, false, TimeSpan.FromHours(10), TimeSpan.FromHours(18)),
            };
            var employeeWorkingHours = WeekHoursRange.Create(employeeHoursRanges);
            var employee = Employee.Create(employeeId, companyId, employeeWorkingHours);
            employee.ClearDomainEvents();
            return employee;
        }

        private static Employee CreateEmployeeWithReservations()
        {
            var employee = CreateEmployeeWithNoReservationsAndTimeOffs();
            var reservations = new List<Reservation>
            {
                Reservation.Create(
                    new DateTime(2020, 12, 11, 12, 30, 0),
                    new DateTime(2020, 12, 11, 13, 45, 0),
                    new DateTime(2020, 12, 5, 14, 0, 0)),
                Reservation.Create(
                    new DateTime(2020, 12, 11, 16, 0, 0),
                    new DateTime(2020, 12, 11, 17, 15, 0),
                    new DateTime(2020, 12, 5, 14, 0, 0))
            };
            reservations.ForEach(reservation => employee.AddReservation(reservation));
            employee.ClearDomainEvents();
            return employee;
        }

        private static Employee CreateEmployeeWithReservationsAndTimeOffs()
        {
            var employee = CreateEmployeeWithReservations();
            var timeOff = TimeOff.Create(
                new DateTime(2020, 12, 10, 12, 30, 0),
                new DateTime(2020, 12, 12, 13, 45, 0),
                new DateTime(2020, 12, 5, 14, 0, 0));
            employee.GiveTimeOff(timeOff);
            employee.ClearDomainEvents();
            return employee;
        }
    }
}
