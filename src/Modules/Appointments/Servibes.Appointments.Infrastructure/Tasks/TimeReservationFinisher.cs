using System.Linq;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.TimeReservations;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Services;

namespace Servibes.Appointments.Infrastructure.Tasks
{
    public class TimeReservationFinisher : IInvocable
    {
        private readonly AppointmentsContext _appointmentsContext;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTimeServer;

        public TimeReservationFinisher(
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
            var timeReservations = await _appointmentsContext.TimeReservations.ToListAsync();

            var timeReservation = timeReservations.FirstOrDefault(x =>
                x.Status == TimeReservationStatus.Created
                && x.ReservationDate.Start.Date == _dateTimeServer.Now.Date
                && x.ReservationDate.End <= _dateTimeServer.Now);

            if (timeReservation == null)
            {
                return;
            }

            timeReservation.Finish(_dateTimeServer.Now);

            await _appointmentsContext.SaveChangesAsync();
            await _eventProcessor.ProcessAsync(timeReservation.DomainEvents);
        }
    }
}
