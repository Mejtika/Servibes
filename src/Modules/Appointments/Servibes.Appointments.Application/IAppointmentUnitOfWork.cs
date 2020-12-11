using System.Threading;
using System.Threading.Tasks;

namespace Servibes.Appointments.Application
{
    public interface IAppointmentUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
