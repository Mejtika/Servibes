using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Application.Events.External.ClientInformationUpdated
{
    public class ClientInformationUpdatedEventHandler : INotificationHandler<ClientInformationUpdatedEvent>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;

        public ClientInformationUpdatedEventHandler(
            IClientRepository clientRepository,
            IAppointmentUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ClientInformationUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetAsync(notification.ClientId);
            client.Update(notification.FirstName, notification.LastName, notification.Email);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}