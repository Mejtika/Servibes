using System;
using System.Threading.Tasks;
using Servibes.Availability.Application;
using Servibes.Shared.Communication;

namespace Servibes.Availability.Infrastructure
{
    public class ReservationApiClient : IReservationApiClient
    {
        private readonly IModuleClient _client;

        public ReservationApiClient(IModuleClient client)
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
