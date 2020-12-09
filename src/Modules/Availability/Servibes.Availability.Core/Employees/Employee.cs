using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Servibes.Availability.Core.Employees.Events;
using Servibes.Availability.Core.Employees.Exceptions;
using Servibes.Availability.Core.Shared;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core.Employees
{
    public class Employee : Entity, IAggregateRoot
    {
        public Guid EmployeeId { get; private set; }

        private Guid _companyId;

        private List<HoursRange> _workingHours;

        private ISet<Reservation> _reservations;

        private ISet<TimeOff> _timeOffs;

        private Employee()
        {
            _workingHours = new List<HoursRange>();
            _reservations = new HashSet<Reservation>();
            _timeOffs = new HashSet<TimeOff>();
        }

        private Employee(Guid employeeId, Guid companyId, List<HoursRange> workingHours)
        {
            EmployeeId = employeeId;
            _companyId = companyId;
            _workingHours = workingHours;
            _reservations = new HashSet<Reservation>();
            _timeOffs = new HashSet<TimeOff>();
        }

        public static Employee Create(Guid employeeId, Guid companyId, List<HoursRange> workingHours)
        {
            CheckForDaysCorrectness(workingHours);
            var resource = new Employee(employeeId, companyId, workingHours);
            resource.AddDomainEvent(new EmployeeAvailabilityCreatedDomainEvent(resource));
            return resource;
        }

        public void AddReservation(Reservation reservation)
        { 
            if (_timeOffs.Any(HasCollidingReservation) ||
                !reservation.IsInWeekWorkingRange(_workingHours) ||
                _reservations.Any(IsColliding))
            {
                AddDomainEvent(new EmployeeReservationCanceledDomainEvent(this, reservation));
                return;
            }

            if (_reservations.Add(reservation))
            {
                AddDomainEvent(new EmployeeReservationAddedDomainEvent(this, reservation));
            }

            bool IsColliding(Reservation r) => r.IsCollidingWith(reservation);

            bool HasCollidingReservation(TimeOff t) => t.IsCollidingWith(reservation);
        }

        public void ReleaseReservation(Reservation reservation)
        {
            if (!_reservations.Remove(reservation))
            {
                return;
            }

            AddDomainEvent(new EmployeeReservationReleasedDomainEvent(this, reservation));
        }

        public void GiveTimeOff(TimeOff timeOff)
        {
            if (_timeOffs.Any(IsColliding))
            {
                throw new TimeOffCollidingDatesException(this, timeOff);
            }

            if (_timeOffs.Add(timeOff))
            {
                AddDomainEvent(new EmployeeTimeOffReceivedDomainEvent(this, timeOff));
            }

            bool IsColliding(TimeOff t) => t.IsCollidingWith(timeOff);
        }

        public void ReleaseTimeOff(TimeOff timeOff)
        {
            if (!_timeOffs.Remove(timeOff))
            {
                return;
            }

            AddDomainEvent(new EmployeeTimeOffReleasedDomainEvent(this, timeOff));
        }

        public void Delete()
        {
            foreach (var reservation in _reservations)
            {
                AddDomainEvent(new EmployeeReservationCanceledDomainEvent(this, reservation));
            }

            AddDomainEvent(new EmployeeAvailabilityDeletedDomainEvent(this));
        }

        public void ChangeWorkingHours(List<HoursRange> companyOpeningHours, List<HoursRange> newWorkingHours)
        {
            CheckForDaysCorrectness(companyOpeningHours);
            CheckForDaysCorrectness(newWorkingHours);

            var canChangeWorkingHours = CanChangeWorkingHours(companyOpeningHours, newWorkingHours);

            if (!canChangeWorkingHours)
            {
                throw new IncorrectWorkingHoursException("Employee working hours are colliding with company opening hours.");
            }

            _workingHours = newWorkingHours;
            AddDomainEvent(new EmployeeWorkingHoursChangedDomainEvent(this));
        }

        public void AdjustWorkingHours(List<HoursRange> companyOpeningHours)
        {
            CheckForDaysCorrectness(companyOpeningHours);
            _workingHours = companyOpeningHours;
            AddDomainEvent(new EmployeeWorkingHoursChangedDomainEvent(this));
        }

        private bool CanChangeWorkingHours(List<HoursRange> companyOpeningHours, List<HoursRange> newWorkingHours)
        {
            return companyOpeningHours
                .Select(openingHourRange =>
                {
                    var newWorkingHourRange = newWorkingHours
                        .SingleOrDefault(workingHoursRange => workingHoursRange.DayOfWeek == openingHourRange.DayOfWeek);
                    return AreWorkingHoursCorrect(openingHourRange, newWorkingHourRange);
                }).ToList().All(x => x);
        }

        private bool AreWorkingHoursCorrect(HoursRange openingHourRange, HoursRange newWorkingHourRange)
            => (openingHourRange.IsAvailable, newWorkingHourRange.IsAvailable) switch
            {
                (true, true) => newWorkingHourRange.IsWithin(openingHourRange.Start, openingHourRange.End),
                (false, false) => true,
                (true, false) => true,
                _ => throw new IncorrectWorkingHoursException($"Found mismatch between employee's and company days availability."),
            };

        private static void CheckForDaysCorrectness(List<HoursRange> weekHoursRanges)
        {
            if (weekHoursRanges.Count != Enum.GetNames(typeof(DayOfWeek)).Length)
            {
                throw new IncorrectHoursRangesException("Wrong number of week days.");
            }

            var daysOfTheWeek = Enum.GetNames(typeof(DayOfWeek)).ToList();
            var daysInWorkingHours = weekHoursRanges.Select(x => x.DayOfWeek.ToString()).ToList();
            if (daysOfTheWeek.Except(daysInWorkingHours).Any())
            {
                throw new IncorrectHoursRangesException("Missing some week day.");
            }
        }
    }
}
