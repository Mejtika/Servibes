using System;

namespace Servibes.Availability.Api.Requests
{
    public class MakeAppointmentRequest
    {
        public Guid ReserveeId { get; set; }

        public Guid ServiceId { get; set; }

        public DateTime Start { get; set; }
    }
}