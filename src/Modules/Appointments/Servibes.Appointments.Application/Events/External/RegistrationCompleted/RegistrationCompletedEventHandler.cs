using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Application.Events.External.RegistrationCompleted
{
    public class RegistrationCompletedEventHandler : INotificationHandler<RegistrationCompletedEvent>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;

        public RegistrationCompletedEventHandler(
            ICompanyRepository companyRepository,
            IAppointmentUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegistrationCompletedEvent notification, CancellationToken cancellationToken)
        {
            var company = Company.Create(notification.CompanyId, notification.WalkInClientId);
            await _companyRepository.AddAsync(company);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}