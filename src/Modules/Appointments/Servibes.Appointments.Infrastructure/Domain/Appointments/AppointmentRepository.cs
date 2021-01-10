using System;
using System.Linq;
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

        public async Task<Appointment> GetByIdAsync(Guid appointmentId)
        {
            return await _appointmentsContext.Appointments.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
        }

        public async Task<Appointment> GetAsync(Guid companyId, Guid employeeId, DateTime start)
        {
            var appointments = await _appointmentsContext.Appointments.ToListAsync();
            return appointments.SingleOrDefault(x => x.CompanyId == companyId && x.Employee.EmployeeId == employeeId && x.ReservationDate.Start == start);
        }
    }
}
