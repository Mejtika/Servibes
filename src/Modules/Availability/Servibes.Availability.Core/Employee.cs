using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Servibes.Availability.Core.Events;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
{
    public class Employee : Entity, IAggregateRoot
    {
        public Guid EmployeeId { get; private set; }

        private Guid _companyId;

        public WeekHoursRange WorkingHours { get; private set; }

        private ISet<Reservation> _reservations = new HashSet<Reservation>();

        public IEnumerable<Reservation> Reservations
        {
            get => _reservations;
            private set => _reservations = new HashSet<Reservation>(value);
        }

        private Employee(Guid employeeId, Guid companyId, WeekHoursRange workingHours)
        {
            EmployeeId = employeeId;
            _companyId = companyId;
            WorkingHours = workingHours;
            Reservations = Enumerable.Empty<Reservation>();
        }

        public static Employee Create(Guid employeeId, Guid companyId, WeekHoursRange workingHours)
        {
            var resource = new Employee(employeeId, companyId, workingHours);
            resource.AddDomainEvent(new EmployeeAvailabilityCreated(resource));
            return resource;
        }

        public void AddReservation(Reservation reservation)
        { 
            if (_reservations.Any(IsColliding) && IsInWeekWorkingRange(reservation))
            {
                AddDomainEvent(new EmployeeReservationCanceled(this, reservation));
                return;
            }

            if (_reservations.Add(reservation))
            {
                AddDomainEvent(new EmployeeReservationAdded(this, reservation));
            }

            bool IsColliding(Reservation r) => r.IsCollidingWith(reservation);
        }

        private bool IsInWeekWorkingRange(Reservation reservation)
            => !reservation.IsLongPeriodReservation() && !reservation.IsOutOfRange(WorkingHours.HoursRanges);

        public void ChangeWorkingHours(WeekHoursRange companyOpeningHours, WeekHoursRange newWorkingHours)
        {
            var areWorkingHoursWithin = companyOpeningHours.HoursRanges.Select(x =>
                newWorkingHours.HoursRanges.SingleOrDefault(y => y.DayOfWeek == x.DayOfWeek).IsWithin(x)).All(x => x);

            if (!areWorkingHoursWithin)
            {
                throw new Exception("Employee working hours are colliding with company opening hours.");
            }

            WorkingHours = newWorkingHours;
        }

        public void AdjustWorkingHours(WeekHoursRange companyOpeningHours)
        {
            WorkingHours = companyOpeningHours;
        }

        public void ReleaseReservation(Reservation reservation)
        {
            if (!_reservations.Remove(reservation))
            {
                return;
            }

            AddDomainEvent(new EmployeeReservationReleased(this, reservation));
        }

        public void Delete()
        {
            foreach (var reservation in Reservations)
            {
                AddDomainEvent(new EmployeeReservationCanceled(this, reservation));
            }

            AddDomainEvent(new EmployeeAvailabilityDeleted(this));
        }
    }
}
