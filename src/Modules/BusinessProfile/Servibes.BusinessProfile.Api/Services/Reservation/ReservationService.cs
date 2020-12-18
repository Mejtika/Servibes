using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Services
{
    public class ReservationService
    {
        private readonly BusinessProfileContext _context;

        public ReservationService(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task<ReservationDataDto> GetReservationData(Guid employeeId, Guid serviceId)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.EmployeeId == employeeId);
            var service = await _context.Services.SingleOrDefaultAsync(x => x.ServiceId == serviceId);

            if (employee == null || service == null)
            {
                throw new Exception("Incorrect reservation data.");
            }

            var reservationData = new ReservationDataDto
            {
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                ServiceName = service.ServiceName,
                ServicePrice = service.Price,
                ServiceDuration = service.Duration
            };

            return reservationData;
        }
    }
}
