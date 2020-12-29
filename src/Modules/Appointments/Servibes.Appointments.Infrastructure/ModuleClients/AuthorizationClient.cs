using System;
using System.Threading.Tasks;
using Servibes.Appointments.Application.ModuleClients;
using Servibes.Sales.Api;
using Servibes.Shared.Communication;

namespace Servibes.Appointments.Infrastructure.ModuleClients
{
    public class AuthorizationClient : IAuthorizationClient
    {
        private readonly IModuleClient _client;

        public AuthorizationClient(IModuleClient client)
        {
            _client = client;
        }

        public async Task<AuthorizationDto> IsAuthenticatedAsync(Guid userId, Guid companyId)
        {
            return await _client.GetAsync<AuthorizationDto>("modules/business/auth",
                new {UserId = userId, CompanyId = companyId});
        }
    }
}