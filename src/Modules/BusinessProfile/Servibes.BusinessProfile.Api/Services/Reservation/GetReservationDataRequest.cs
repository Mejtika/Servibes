using System;

namespace Servibes.BusinessProfile.Api.Services.Reservation
{
    public class GetReservationDataRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid ServiceId { get; set; }
    }
}