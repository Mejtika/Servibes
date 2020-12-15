using System;

namespace Servibes.Appointments.Api
{
    public class MakeAppointmentRequest
    {
        public Guid ReserveeId { get; set; }

        public string EmployeeName { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public int ServiceDuration { get; set; }

        public DateTime Start { get; set; }
    }
}