using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Sales.Api.Models;

namespace Servibes.Sales.Api.Events.External.AppointmentFinished
{
    public class AppointmentFinishedEventHandler : INotificationHandler<AppointmentFinishedEvent>
    {
        private readonly SalesContext _context;

        public AppointmentFinishedEventHandler(SalesContext context)
        {
            _context = context;
        }

        public async Task Handle(AppointmentFinishedEvent notification, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                AppointmentId = notification.AppointmentId,
                ReserveeId = notification.ReserveeId,
                CompanyId = notification.CompanyId,
                EmployeeId = notification.EmployeeId,
                Price = notification.ServicePrice,
                ServiceName = notification.ServiceName,
                Status = AppointmentStatus.Unpaid
            };

            await _context.Appointments.AddAsync(appointment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}