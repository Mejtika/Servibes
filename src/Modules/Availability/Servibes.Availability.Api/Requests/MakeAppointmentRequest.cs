using System;

namespace Servibes.Appointments.Api
{
    public class MakeAppointmentRequest
    {
        public Guid ReserveeId { get; set; }

        public Guid ServiceId { get; set; }

        public DateTime Start { get; set; }
    }
}