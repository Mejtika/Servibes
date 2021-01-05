using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Reservees;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Application.Events.External.ReservationAdded
{
    public class ReservationAddedEventHandler : INotificationHandler<ReservationAddedEvent>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IDateTimeServer _dateTime;
        private readonly IEventProcessor _eventProcessor;
        private readonly IMessageBroker _messageBroker;

        public ReservationAddedEventHandler(
            IAppointmentRepository appointmentRepository,
            IClientRepository clientRepository,
            ICompanyRepository companyRepository,
            IAppointmentUnitOfWork unitOfWork,
            IDateTimeServer dateTime,
            IEventProcessor eventProcessor,
            IMessageBroker messageBroker)
        {
            _appointmentRepository = appointmentRepository;
            _clientRepository = clientRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
            _eventProcessor = eventProcessor;
            _messageBroker = messageBroker;
        }

        public async Task Handle(ReservationAddedEvent notification, CancellationToken cancellationToken)
        {
            await CheckReservationCorrectness(notification);

            var employee = Employee.Create(notification.EmployeeId, notification.EmployeeName);
            var service = Service.Create(notification.ServicePrice, notification.ServiceName);
            var reservedDate = ReservationDate.Create(notification.Start, notification.End, _dateTime.Now);
            var appointment = Appointment.Create(
                Guid.NewGuid(),
                notification.ReserveeId,
                notification.CompanyId,
                employee,
                service,
                reservedDate);

            await _appointmentRepository.AddAsync(appointment);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(appointment.DomainEvents);
        }

        private async Task CheckReservationCorrectness(ReservationAddedEvent notification)
        {
            var isWalkInClient = await _companyRepository.ExistsByWalkInIdAsync(notification.CompanyId, notification.ReserveeId);
            var isClient = await _clientRepository.ExistsAsync(notification.ReserveeId);
            if (isWalkInClient || isClient)
            {
                return;
            }

            var @event = new AppointmentRejectedEvent(notification.CompanyId, notification.EmployeeId, notification.Start);
            await _messageBroker.PublishAsync(@event);
            throw new AppException($"Client with id {notification.ReserveeId} not found.");
        }
    }
}