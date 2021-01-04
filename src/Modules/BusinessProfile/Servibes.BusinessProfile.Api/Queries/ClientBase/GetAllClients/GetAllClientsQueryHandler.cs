using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(
            BusinessProfileContext context,
            IMapper mapper)
        {
            this._context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var companyClients = await _context.Clients.FromSqlInterpolated(
                 $@"SELECT DISTINCT
                    [Clients].[ClientId],
                    [Clients].[FirstName],
                    [Clients].[LastName],
                    [Clients].[Email]
                    FROM [Servibes].[business].[Clients] AS [Clients]
                    JOIN [Servibes].[business].[Appointments] AS [Appointments]
                    ON [Clients].ClientId = [Appointments].ClientId
                    WHERE [Appointments].CompanyId = {request.CompanyId}
                    UNION
                    SELECT
                    [Clients].[ClientId],
                    [Clients].[FirstName],
                    [Clients].[LastName],
                    [Clients].[Email]
                    FROM [Servibes].[business].[Clients] AS [Clients]
                    JOIN [Servibes].[business].[Companies] AS [Companies]
                    ON [Clients].ClientId = [Companies].WalkInClientId
                    WHERE [Companies].CompanyId = {request.CompanyId}").ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ClientDto>>(companyClients);
        }
    }
}
