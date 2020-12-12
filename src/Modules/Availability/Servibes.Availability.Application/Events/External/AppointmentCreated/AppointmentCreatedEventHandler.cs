using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Communication.Events;

namespace Servibes.Availability.Application.Events.External.AppointmentCreated
{
    public class AppointmentCreatedEventHandler : INotificationHandler<AppointmentCreatedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;

        public AppointmentCreatedEventHandler(
            IEmployeeRepository employeeRepository, 
            IAvailabilityUnitOfWork unitOfWork,
            IEventProcessor eventProcessor)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _eventProcessor = eventProcessor;
        }

        public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(notification.EmployeeId);
            var reservation = Reservation.Create(notification.Start, notification.End, DateTime.Now);
            employee.AddReservation(reservation);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(employee.DomainEvents);
        }
    }
}