using System;
using System.Collections.Generic;
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

        public Guid CompanyId { get; private set; }

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
            CompanyId = companyId;
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

        public Reservation GetReservationByDate(DateTime start)
        {
            return _reservations.SingleOrDefault(x => x.Start == start);
        }

        public void AddReservation(Reservation reservation, ReservationSnapshot snapshot)
        {
            CheckForCollision(reservation);
             
            if (_reservations.Add(reservation))
            {
                AddDomainEvent(new EmployeeReservationAddedDomainEvent(EmployeeId, CompanyId, reservation, snapshot));
            }
        }

        public void ReleaseReservation(Reservation reservation)
        {
            if (!_reservations.Remove(reservation))
            {
                return;
            }

            AddDomainEvent(new EmployeeReservationReleasedDomainEvent(this, reservation));
        }

        public TimeOff GetTimeOffByDate(DateTime start)
        {
            return _timeOffs.SingleOrDefault(x => x.Start == start);
        }

        public void GiveTimeOff(TimeOff timeOff)
        {
            if (_timeOffs.Any(IsColliding))
            {
                throw new TimeOffCollidingDatesException(timeOff);
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
                AddDomainEvent(new EmployeeReservationReleasedDomainEvent(this, reservation));
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
            var newWorkingHours = Adjust(companyOpeningHours);
            _workingHours = newWorkingHours;
            AddDomainEvent(new EmployeeWorkingHoursChangedDomainEvent(this));
        }

        private List<HoursRange> Adjust(List<HoursRange> newOpeningHours)
        {
            List<HoursRange> newHoursRanges = new List<HoursRange>();
            foreach (var newDayHoursRange in newOpeningHours)
            {
               var workingHoursRange = _workingHours.SingleOrDefault(x => x.DayOfWeek == newDayHoursRange.DayOfWeek);
               if (!newDayHoursRange.IsAvailable)
               {
                   var unavailableHoursRange = HoursRange.Create(
                       newDayHoursRange.DayOfWeek,
                       newDayHoursRange.IsAvailable,
                       workingHoursRange.Start,
                       workingHoursRange.End);
                   newHoursRanges.Add(unavailableHoursRange);
                   continue;
               }

               if (newDayHoursRange.IsAvailable && !workingHoursRange.IsAvailable)
               {
                   newHoursRanges.Add(workingHoursRange);
                   continue;
               }

               var newStart = newDayHoursRange.Start > workingHoursRange.Start
                   ? newDayHoursRange.Start
                   : workingHoursRange.Start;

               var newEnd = newDayHoursRange.End < workingHoursRange.End
                   ? newDayHoursRange.End
                   : workingHoursRange.End;

               var newHoursRange = HoursRange.Create(
                   newDayHoursRange.DayOfWeek, 
                   newDayHoursRange.IsAvailable,
                   newStart,
                   newEnd);

               newHoursRanges.Add(newHoursRange);
            }

            return newHoursRanges;
        }

        public bool CheckCompanyCorrectness(Guid companyId)
            => CompanyId == companyId;
        
        private void CheckForCollision(Reservation reservation)
        {
            if (_timeOffs.Any(HasCollidingReservation) ||
                !reservation.IsInWeekWorkingRange(_workingHours) ||
                _reservations.Any(IsColliding))
            {
                throw new IncorrectReservationDateException(reservation);
            }

            bool IsColliding(Reservation r) => r.IsCollidingWith(reservation);

            bool HasCollidingReservation(TimeOff t) => t.IsCollidingWith(reservation);
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
