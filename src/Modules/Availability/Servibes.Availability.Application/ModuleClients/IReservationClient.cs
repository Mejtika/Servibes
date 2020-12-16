using System;
using System.Threading.Tasks;

namespace Servibes.Availability.Application.ModuleClients
{
    public interface IReservationClient
    {
        Task<ReservationDataDto> GetReservationData(Guid employeeId, Guid serviceId);
    }
}
