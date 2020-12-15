using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models.ClientBase;

namespace Servibes.BusinessProfile.Api.Events.External.AppointmentCreated
{
    public class AppointmentCreatedEventHandler : INotificationHandler<AppointmentCreatedEvent>
    {
        private readonly BusinessProfileContext _context;

        public AppointmentCreatedEventHandler(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == notification.ReserveeId, cancellationToken: cancellationToken);

            var appointment = new Appointment
            {
                AppointmentId = notification.AppointmentId,
                ClientId = notification.ReserveeId,
                CompanyId = notification.CompanyId
            };
            
            client.Appointments.Add(appointment);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}