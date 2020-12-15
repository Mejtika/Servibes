using System;
using System.Threading.Tasks;

namespace Servibes.Availability.Application
{
    public interface IReservationApiClient
    {
        Task<ReservationDataDto> GetReservationData(Guid employeeId, Guid serviceId);
    }
}
