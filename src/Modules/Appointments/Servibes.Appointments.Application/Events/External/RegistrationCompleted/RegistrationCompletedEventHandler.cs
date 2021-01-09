using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Application.Events.External.RegistrationCompleted
{
    public class RegistrationCompletedEventHandler : INotificationHandler<RegistrationCompletedEvent>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;

        public RegistrationCompletedEventHandler(
            ICompanyRepository companyRepository,
            IClientRepository clientRepository,
            IAppointmentUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegistrationCompletedEvent notification, CancellationToken cancellationToken)
        {
            var company = Company.Create(notification.CompanyId, notification.WalkInClientId, notification.OwnerId);
            await _companyRepository.AddAsync(company);
            var walkInClient = Client.Create(notification.WalkInClientId, "Walk-in", "Client", "walkin@walkin.com");
            await _clientRepository.AddAsync(walkInClient);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}