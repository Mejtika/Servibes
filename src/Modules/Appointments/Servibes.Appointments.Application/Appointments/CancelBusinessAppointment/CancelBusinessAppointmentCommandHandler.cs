using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Application;
using Servibes.Appointments.Application.Appointments.CancelBusinessAppointment;
using Servibes.Appointments.Application.ModuleClients;
using Servibes.Appointments.Core.Appointments;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Api
{
    public class CancelBusinessAppointmentCommandHandler : IRequestHandler<CancelBusinessAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;
        private readonly IAuthorizationClient _authorizationClient;
        private readonly IDateTimeServer _dateTimeServer;
        private readonly IEventProcessor _eventProcessor;

        public CancelBusinessAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IAppointmentUnitOfWork unitOfWork,
            IHttpContextAccessor accessor,
            IAuthorizationClient authorizationClient,
            IDateTimeServer dateTimeServer,
            IEventProcessor eventProcessor)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
            _authorizationClient = authorizationClient;
            _dateTimeServer = dateTimeServer;
            _eventProcessor = eventProcessor;
        }

        public async Task<Unit> Handle(CancelBusinessAppointmentCommand request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var appointment = await  _appointmentRepository.GetAsync(request.AppointmentId);
            var user = await _authorizationClient.IsAuthenticatedAsync(ownerId, appointment.CompanyId);
            if (!user.IsAuthorized)
            {
                throw new AppException($"User {ownerId} is not authorized to perform this action.");
            }

            appointment.Cancel(_dateTimeServer.Now, request.CancellationReason);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(appointment.DomainEvents);

            return Unit.Value;
        }
    }
}