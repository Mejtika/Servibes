using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Servibes.Shared.BuildingBlocks;

namespace Servibes.Availability.Core
{
    public class Employee : AggregateRoot
    {
        public Guid EmployeeId { get; set; }

        public WorkingHours WorkingHours { get; set; }

        private ISet<Reservation> _reservations = new HashSet<Reservation>();

        public IEnumerable<Reservation> Reservations
        {
            get => _reservations;
            private set => _reservations = new HashSet<Reservation>(value);
        }

        public Employee(Guid employeeId, List<WorkingHours> workingHours)
        {
            EmployeeId = employeeId;
            Reservations = Enumerable.Empty<Reservation>();
        }

        public static Employee Create(Guid employeeId, List<WorkingHours> workingHours)
        {
            var resource = new Employee(employeeId, workingHours);

            // resource.AddEvent(new ResourceCreated(resource));

            return resource;
        }

        public void AddReservation(Reservation reservation)
        { 
            var isInWeekWorkingRange = IsInWeekWorkingRange(reservation);
            var hasCollidingReservation = _reservations.Any(IsCollidingReservation);
            if (hasCollidingReservation && isInWeekWorkingRange)
            {
                AddEvent(new ReservationCanceled(this, reservation));
                return;
            }

            if (_reservations.Add(reservation))
            {
                AddEvent(new ReservationAdded(this, reservation));
            }

            bool IsCollidingReservation(Reservation r) => r.IsCollidingWith(reservation);

        }

        private bool IsInWeekWorkingRange(Reservation reservation)
            => !reservation.IsLongPeriodReservation() && !WorkingHours.IsOutOfRange(reservation);
        
        public void ChangeAvailability(WorkingHours workingHours) => WorkingHours = workingHours;

        public void ReleaseReservation(Reservation reservation)
        {
            if (!_reservations.Remove(reservation))
            {
                return;
            }

            AddEvent(new ReservationReleased(this, reservation));
        }

        public void Delete()
        {
            foreach (var reservation in Reservations)
            {
                AddEvent(new ReservationCanceled(this, reservation));
            }

            AddEvent(new ResourceDeleted(this));
        }
    }
}
