using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Application.Events.External.NewClientRegistered
{
    public class NewClientRegisteredEventHandler : INotificationHandler<NewClientRegisteredEvent>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;

        public NewClientRegisteredEventHandler(
            IClientRepository clientRepository,
            IAppointmentUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(NewClientRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var client = Client.Create(notification.ClientId, notification.FirstName, notification.LastName, notification.Email);
            await _clientRepository.AddAsync(client);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}