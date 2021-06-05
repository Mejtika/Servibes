using System;
using System.Linq;
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

        public async Task<TimeReservation> GetByIdAsync(Guid timeReservationId)
        {
            return await _appointmentsContext.TimeReservations.SingleOrDefaultAsync(x =>
                x.TimeReservationId == timeReservationId);
        }

        public async Task<TimeReservation> GetAsync(Guid companyId, Guid employeeId, DateTime start)
        {
            var timeReservations =  await _appointmentsContext.TimeReservations.ToListAsync();
            return timeReservations.SingleOrDefault(x => x.CompanyId == companyId && x.EmployeeId == employeeId && x.ReservedDate.Start == start);
        }
    }
}
