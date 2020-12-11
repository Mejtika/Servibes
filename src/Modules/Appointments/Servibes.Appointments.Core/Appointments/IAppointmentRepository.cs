using System.Threading.Tasks;

namespace Servibes.Appointments.Core.Appointments
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
    }
}
