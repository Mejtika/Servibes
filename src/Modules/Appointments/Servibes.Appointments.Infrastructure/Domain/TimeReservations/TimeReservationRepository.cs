using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.TimeReservations;

namespace Servibes.Appointments.Infrastructure.Domain.TimeReservations
{
    public class TimeReservationRepository : ITimeReservationRepository
    {
        private readonly AppointmentsContext _appointmentsContext;

        public TimeReservationRepository(AppointmentsContext appointmentsContext)
        {
            _appointmentsContext = appointmentsContext;
        }

        public async Task AddAsync(TimeReservation timeReservation)
        {
            await _appointmentsContext.TimeReservations.AddAsync(timeReservation);
        }

        public async Task<TimeReservation> GetAsync(Guid timeReservationId)
        {
            return await _appointmentsContext.TimeReservations.SingleOrDefaultAsync(x =>
                x.TimeReservationId == timeReservationId);
        }
    }
}
