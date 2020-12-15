using System.Threading.Tasks;
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
            await _appointmentsContext.AddAsync(appointment);
        }
    }
}
