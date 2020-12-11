using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Application.Events.External.RegistrationCompleted
{
    public class RegistrationCompletedEventHandler : INotificationHandler<RegistrationCompletedEvent>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public RegistrationCompletedEventHandler(
            ICompanyRepository companyRepository,
            IAvailabilityUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegistrationCompletedEvent notification, CancellationToken cancellationToken)
        {
            var openingHours =
                notification.OpeningHoursDto.Select(x => 
                    HoursRange.Create(x.DayOfWeek, x.IsAvailable, TimeSpan.Parse(x.Start), TimeSpan.Parse(x.End))).ToList();
            var company = Company.Create(notification.CompanyId, openingHours);
            await _companyRepository.AddAsync(company);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}