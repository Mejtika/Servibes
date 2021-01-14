using System;
using System.Threading.Tasks;

namespace Servibes.Appointments.Core.Reservees
{
    public interface IClientRepository
    {
        Task<bool> ExistsAsync(Guid clientId);

        Task AddAsync(Client client);

        Task<Client> GetAsync(Guid clientId);
    }
}
