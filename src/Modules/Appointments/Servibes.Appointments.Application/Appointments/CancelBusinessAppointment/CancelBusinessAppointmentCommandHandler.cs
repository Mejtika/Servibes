using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Servibes.Appointments.Application;
using Servibes.Appointments.Application.Appointments.CancelBusinessAppointment;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Reservees;
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
        private readonly ICompanyRepository _companyRepository;
        private readonly IDateTimeServer _dateTimeServer;
        private readonly IEventProcessor _eventProcessor;

        public CancelBusinessAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IAppointmentUnitOfWork unitOfWork,
            IHttpContextAccessor accessor,
            ICompanyRepository companyRepository,
            IDateTimeServer dateTimeServer,
            IEventProcessor eventProcessor)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
            _companyRepository = companyRepository;
            _dateTimeServer = dateTimeServer;
            _eventProcessor = eventProcessor;
        }

        public async Task<Unit> Handle(CancelBusinessAppointmentCommand request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var appointment = await  _appointmentRepository.GetByIdAsync(request.AppointmentId);
            var isAuthorized = await _companyRepository.ExistsByOwnerIdAsync(appointment.CompanyId, ownerId);
            if (!isAuthorized)
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