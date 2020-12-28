using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Application.ModuleClients;
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
        private readonly IAuthorizationClient _authorizationClient;
        private readonly IDateTimeServer _dateTimeServer;
        private readonly IEventProcessor _eventProcessor;

        public CancelBusinessTimeReservationCommandHandler(
            ITimeReservationRepository timeReservationRepository, 
            IAppointmentUnitOfWork unitOfWork, 
            IHttpContextAccessor accessor, 
            IAuthorizationClient authorizationClient, 
            IDateTimeServer dateTimeServer, 
            IEventProcessor eventProcessor)
        {
            _timeReservationRepository = timeReservationRepository;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
            _authorizationClient = authorizationClient;
            _dateTimeServer = dateTimeServer;
            _eventProcessor = eventProcessor;
        }

        public async Task<Unit> Handle(CancelBusinessTimeReservationCommand request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var timeReservation = await _timeReservationRepository.GetAsync(request.TimeReservationId);
            var user = await _authorizationClient.IsAuthenticatedAsync(ownerId, timeReservation.CompanyId);
            if (!user.IsAuthorized)
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