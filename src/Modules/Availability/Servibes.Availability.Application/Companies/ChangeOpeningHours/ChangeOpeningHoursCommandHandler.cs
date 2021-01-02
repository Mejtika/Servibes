using MediatR;
using Servibes.Availability.Application.Events.Companies;
using Servibes.Availability.Application.Shared;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Companies.Events;
using Servibes.Availability.Core.Shared;
using Servibes.Shared.BuildingBlocks;
using Servibes.Shared.Communication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.Availability.Application.Companies.ChangeOpeningHours
{
    public class ChangeOpeningHoursCommandHandler : IRequestHandler<ChangeOpeningHoursCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventMapper _eventMapper;
        private readonly IMediator _mediator;

        public ChangeOpeningHoursCommandHandler(
            ICompanyRepository companyRepository,
            IAvailabilityUnitOfWork unitOfWork,
            IEventMapper eventMapper,
            IMediator mediator)
        {
            this._companyRepository = companyRepository;
            this._unitOfWork = unitOfWork;
            this._eventMapper = eventMapper;
            this._mediator = mediator;
        }

        public async Task<Unit> Handle(ChangeOpeningHoursCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company is null)
                throw new ArgumentNullException($"Company with id {request.CompanyId} doesnt exist.");

            company.ChangeOpeningHours(request.OpeningHours
                    .Select(x => HoursRange.Create(x.DayOfWeek, x.IsAvailable, TimeSpan.Parse(x.Start), TimeSpan.Parse(x.End)))
                    .ToList());

            await _unitOfWork.CommitAsync(cancellationToken);

            var @event = MapEvent(company.DomainEvents);

            await _mediator.Publish(@event, cancellationToken);

            return await Unit.Task;
        }

        private CompanyAvailabilityChangedEvent MapEvent(IEnumerable<IDomainEvent> @events)
        {
            var @event = @events.First() as CompanyOpeningHoursChanged;

            var hoursRangeDtos = @event.Company.GetOpeningHours()
                .Select(x =>
                    new HoursRangeDto()
                    {
                        DayOfWeek = x.DayOfWeek,
                        IsAvailable = x.IsAvailable,
                        Start = x.Start.ToString(),
                        End = x.End.ToString()
                    }).ToList();

            return new CompanyAvailabilityChangedEvent(hoursRangeDtos, @event.Company.CompanyId);
        }
    }
}
