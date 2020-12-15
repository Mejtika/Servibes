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
            var appointment = new Appointment
            {
                AppointmentId = notification.AppointmentId,
                ClientId = notification.ReserveeId,
                CompanyId = notification.CompanyId
            };
            await _context.Appointments.AddAsync(appointment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}