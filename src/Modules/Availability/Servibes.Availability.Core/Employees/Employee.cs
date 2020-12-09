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

        public WeekHoursRange WorkingHours { get; private set; }

        private ISet<Reservation> _reservations = new HashSet<Reservation>();

        private ISet<TimeOff> _timeOffs = new HashSet<TimeOff>();

        private Employee(Guid employeeId, Guid companyId, WeekHoursRange workingHours)
        {
            EmployeeId = employeeId;
            _companyId = companyId;
            WorkingHours = workingHours;
        }

        public static Employee Create(Guid employeeId, Guid companyId, WeekHoursRange workingHours)
        {
            var resource = new Employee(employeeId, companyId, workingHours);
            resource.AddDomainEvent(new EmployeeAvailabilityCreatedDomainEvent(resource));
            return resource;
        }

        public void AddReservation(Reservation reservation)
        { 
            if (_timeOffs.Any(HasCollidingReservation) ||
                !reservation.IsInWeekWorkingRange(WorkingHours.HoursRanges) ||
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

        public void ChangeWorkingHours(WeekHoursRange companyOpeningHours, WeekHoursRange newWorkingHours)
        {
            var canChangeWorkingHours = CanChangeWorkingHours(companyOpeningHours, newWorkingHours);

            if (!canChangeWorkingHours)
            {
                throw new IncorrectWorkingHoursException("Employee working hours are colliding with company opening hours.");
            }

            WorkingHours = newWorkingHours;
        }

        public void AdjustWorkingHours(WeekHoursRange companyOpeningHours)
        {
            WorkingHours = companyOpeningHours;
        }

        private bool CanChangeWorkingHours(WeekHoursRange companyOpeningHours, WeekHoursRange newWorkingHours)
        {
            return companyOpeningHours.HoursRanges
                .Select(openingHourRange =>
                {
                    var newWorkingHourRange = newWorkingHours
                        .HoursRanges
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
    }
}
