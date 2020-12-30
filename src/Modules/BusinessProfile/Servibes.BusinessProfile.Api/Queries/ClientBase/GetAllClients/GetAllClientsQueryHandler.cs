using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly BusinessProfileContext _context;

        public GetAllClientsQueryHandler(
            BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Appointments.Join(_context.Clients,
                    appointment => appointment.ClientId,
                    client => client.ClientId,
                    (appointmemnt, client) => new ClientDto
                    {
                        ClientId = client.ClientId,
                        Email = client.Email,
                        FirstName = client.FirstName,
                        LastName = client.LastName
                    }).Distinct().ToListAsync<ClientDto>();

            var walkInClient = await _context.Companies.Where(c => c.CompanyId == request.CompanyId).Join(_context.Clients,
                company => company.WalkInClientId,
                client => client.ClientId,
                (company, client) => new ClientDto
                {
                    ClientId = client.ClientId,
                    Email = client.Email,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                }).Distinct().FirstOrDefaultAsync();

            if (walkInClient is null)
                throw new Exception("Company doesnt have a walk in client specified.");

            clients.Add(walkInClient);

            return clients;
        }
    }
}
