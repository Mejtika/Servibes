using System.Threading;
using System.Threading.Tasks;
using Servibes.Availability.Application;

namespace Servibes.Availability.Infrastructure
{
    public class AvailabilityUnitOfWork : IAvailabilityUnitOfWork
    {
        private readonly AvailabilityContext _context;

        public AvailabilityUnitOfWork(AvailabilityContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
