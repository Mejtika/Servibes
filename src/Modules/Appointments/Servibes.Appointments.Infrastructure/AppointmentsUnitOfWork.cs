using System.Threading;
using System.Threading.Tasks;
using Servibes.Appointments.Application;

namespace Servibes.Appointments.Infrastructure
{
    public class AppointmentsUnitOfWork : IAppointmentUnitOfWork
    {
        private readonly AppointmentsContext _context;

        public AppointmentsUnitOfWork(AppointmentsContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
