using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Infrastructure.Domain.Reservees
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppointmentsContext _appointmentsContext;

        public ClientRepository(AppointmentsContext appointmentsContext)
        {
            _appointmentsContext = appointmentsContext;
        }

        public async Task<bool> ExistsAsync(Guid clientId)
        {
            return await _appointmentsContext.Clients.AnyAsync(x => x.ClientId == clientId);
        }

        public async Task AddAsync(Client client)
        {
            await _appointmentsContext.Clients.AddAsync(client);
        }
    }
}
