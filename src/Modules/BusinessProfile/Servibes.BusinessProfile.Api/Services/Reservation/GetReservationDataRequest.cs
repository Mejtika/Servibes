using System;

namespace Servibes.BusinessProfile.Api.Services
{
    public class GetReservationDataRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }
    }
}