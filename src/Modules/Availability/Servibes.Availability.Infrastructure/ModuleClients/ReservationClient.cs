using System;
using System.Threading.Tasks;
using Servibes.Availability.Application.ModuleClients;
using Servibes.Shared.Communication;

namespace Servibes.Availability.Infrastructure.ModuleClients
{
    public class ReservationClient : IReservationClient
    {
        private readonly IModuleClient _client;

        public ReservationClient(IModuleClient client)
        {
            _client = client;
        }

        public async Task<ReservationDataDto> GetReservationData(Guid employeeId, Guid serviceId)
        {
           return await _client.GetAsync<ReservationDataDto>("modules/business/details",
                new {EmployeeId = employeeId, ServiceId = serviceId});
        }
    }
}
