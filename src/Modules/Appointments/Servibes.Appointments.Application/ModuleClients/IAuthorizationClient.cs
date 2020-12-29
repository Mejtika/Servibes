using System;
using System.Threading.Tasks;
using Servibes.Sales.Api;

namespace Servibes.Appointments.Application.ModuleClients
{
    public interface IAuthorizationClient
    {
        Task<AuthorizationDto> IsAuthenticatedAsync(Guid userId, Guid companyId);
    }
}