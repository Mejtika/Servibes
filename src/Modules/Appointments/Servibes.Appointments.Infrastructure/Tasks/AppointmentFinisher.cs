using System;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.Appointments;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Infrastructure.Tasks
{
    public class AppointmentFinisher : IInvocable
    {
        private readonly AppointmentsContext _appointmentsContext;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTimeServer;

        public AppointmentFinisher(
            AppointmentsContext appointmentsContext,
            IEventProcessor eventProcessor,
            IDateTimeServer dateTimeServer)
        {
            _appointmentsContext = appointmentsContext;
            _eventProcessor = eventProcessor;
            _dateTimeServer = dateTimeServer;
        }

        public async Task Invoke()
        {
            var appointments = await _appointmentsContext.Appointments.ToListAsync();

            var appointment = appointments.FirstOrDefault(x =>
                x.Status == AppointmentStatus.Confirmed
                && x.ReservedDate.Start.Date == _dateTimeServer.Now.Date
                && x.ReservedDate.End <= _dateTimeServer.Now);

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
