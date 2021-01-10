using System;
using System.Threading.Tasks;

namespace Servibes.Appointments.Core.Appointments
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);

        Task<Appointment> GetByIdAsync(Guid appointmentId);

        Task<Appointment> GetAsync(Guid companyId, Guid employeeId, DateTime start);
    }
}
