using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Shared;
using Servibes.Shared.Communication.Events;

namespace Servibes.Appointments.Application.Appointments.MakeAppointment
{
    public class MakeAppointmentCommandHandler : IRequestHandler<MakeAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;

        public MakeAppointmentCommandHandler(
            IAppointmentRepository appointmentRepository,
            IAppointmentUnitOfWork unitOfWork,
            IEventProcessor eventProcessor)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _eventProcessor = eventProcessor;
        }

        public async Task<Unit> Handle(MakeAppointmentCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid();
            var employee = Employee.Create(request.EmployeeId, request.EmployeeName);
            var service = Service.Create(request.ServicePrive, request.ServiceName);
            var reservationDate =
                ReservationDate.Create(request.Start, request.Start.AddMinutes(request.ServiceDuration), DateTime.Now);
            var appointment = Appointment.Create(Guid.NewGuid(), userId, request.CompanyId, employee, service,
                reservationDate);
            await _appointmentRepository.AddAsync(appointment);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(appointment.DomainEvents);
            return Unit.Value;
        }
    }
}
