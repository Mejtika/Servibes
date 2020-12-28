using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Core.Appointments;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Application.Appointments.CancelClientAppointment
{
    public class CancelClientAppointmentCommandHandler : IRequestHandler<CancelClientAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IHttpContextAccessor _accessor;
        private readonly IDateTimeServer _dateTimeServer;

        public CancelClientAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IAppointmentUnitOfWork unitOfWork,
            IEventProcessor eventProcessor,
            IHttpContextAccessor accessor,
            IDateTimeServer dateTimeServer)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _eventProcessor = eventProcessor;
            _accessor = accessor;
            _dateTimeServer = dateTimeServer;
        }

        public async Task<Unit> Handle(CancelClientAppointmentCommand request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var appointment = await _appointmentRepository.GetAsync(request.AppointmentId);
            var isOwner = appointment.ReserveeId == ownerId;
            if (!isOwner)
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