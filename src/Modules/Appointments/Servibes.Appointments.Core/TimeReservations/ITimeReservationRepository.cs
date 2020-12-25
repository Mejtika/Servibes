using System;
using System.Threading.Tasks;

namespace Servibes.Appointments.Core.TimeReservations
{
    public interface ITimeReservationRepository
    {
        Task AddAsync(TimeReservation timeReservation);

        Task<TimeReservation> GetAsync(Guid timeReservationId);
    }
}
