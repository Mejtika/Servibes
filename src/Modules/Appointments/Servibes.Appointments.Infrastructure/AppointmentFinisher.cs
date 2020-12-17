using System;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.Appointments;
using Servibes.Shared.Communication.Events;

namespace Servibes.Appointments.Infrastructure
{
    public class AppointmentFinisher : IInvocable
    {
        private readonly AppointmentsContext _appointmentsContext;
        private readonly IEventProcessor _eventProcessor;

        public AppointmentFinisher(
            AppointmentsContext appointmentsContext,
            IEventProcessor eventProcessor)
        {
            _appointmentsContext = appointmentsContext;
            _eventProcessor = eventProcessor;
        }

        public async Task Invoke()
        {
            var appointments = await _appointmentsContext.Appointments.ToListAsync();

            var appointment = appointments.FirstOrDefault(x =>
                x.AppointmentStatus == AppointmentStatus.Confirmed
                && x.ReservationDate.Start.Date == DateTime.Today
                && x.ReservationDate.End <= DateTime.Now);

            if (appointment == null)
            {
                return;
            }

            appointment.Finish(DateTime.Now);

            await _appointmentsContext.SaveChangesAsync();
            await _eventProcessor.ProcessAsync(appointment.DomainEvents);
        }
    }
}
