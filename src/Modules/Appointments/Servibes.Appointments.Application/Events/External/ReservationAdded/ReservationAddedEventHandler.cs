using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Appointments.Core.Appointments;

namespace Servibes.Appointments.Application.Events.External.ReservationAdded
{
    public class ReservationAddedEventHandler : INotificationHandler<ReservationAddedEvent>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentUnitOfWork _unitOfWork;

        public ReservationAddedEventHandler(
            IAppointmentRepository appointmentRepository,
            IAppointmentUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(ReservationAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}