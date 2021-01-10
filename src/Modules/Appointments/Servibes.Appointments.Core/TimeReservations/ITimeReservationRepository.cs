using System;
using System.Threading.Tasks;

namespace Servibes.Appointments.Core.TimeReservations
{
    public interface ITimeReservationRepository
    {
        Task AddAsync(TimeReservation timeReservation);

        Task<TimeReservation> GetByIdAsync(Guid timeReservationId);

        Task<TimeReservation> GetAsync(Guid companyId, Guid employeeId, DateTime start);
    }
}
