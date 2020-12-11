using System.Threading;
using System.Threading.Tasks;

namespace Servibes.Availability.Application
{
    public interface IAvailabilityUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
