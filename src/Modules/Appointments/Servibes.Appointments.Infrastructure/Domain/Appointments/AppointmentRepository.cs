using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.Appointments;

namespace Servibes.Appointments.Infrastructure.Domain.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentsContext _appointmentsContext;

        public AppointmentRepository(AppointmentsContext appointmentsContext)
        {
            _appointmentsContext = appointmentsContext;
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _appointmentsContext.Appointments.AddAsync(appointment);
        }

        public async Task<Appointment> GetAsync(Guid appointmentId)
        {
            return await _appointmentsContext.Appointments.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
        }
    }
}
