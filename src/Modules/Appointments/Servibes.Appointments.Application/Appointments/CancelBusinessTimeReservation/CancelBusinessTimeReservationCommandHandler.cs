using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Core.Reservees;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Application.Appointments.CancelBusinessTimeReservation
{
    public class CancelBusinessTimeReservationCommandHandler : IRequestHandler<CancelBusinessTimeReservationCommand>
    {
        private readonly ITimeReservationRepository _timeReservationRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDateTimeServer _dateTimeServer;
        private readonly IEventProcessor _eventProcessor;

        public CancelBusinessTimeReservationCommandHandler(
            ITimeReservationRepository timeReservationRepository, 
            IAppointmentUnitOfWork unitOfWork, 
            IHttpContextAccessor accessor, 
            ICompanyRepository companyRepository,
            IDateTimeServer dateTimeServer, 
            IEventProcessor eventProcessor)
        {
            _timeReservationRepository = timeReservationRepository;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
            _companyRepository = companyRepository;
            _dateTimeServer = dateTimeServer;
            _eventProcessor = eventProcessor;
        }

        public async Task<Unit> Handle(CancelBusinessTimeReservationCommand request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var timeReservation = await _timeReservationRepository.GetAsync(request.TimeReservationId);
            var isAuthorized = await _companyRepository.ExistsByWalkInIdAsync(timeReservation.CompanyId, ownerId);
            if (!isAuthorized)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            timeReservation.Cancel(_dateTimeServer.Now);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(timeReservation.DomainEvents);

            return Unit.Value;
        }
    }
}