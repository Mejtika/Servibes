using System;

namespace Servibes.Appointments.Api
{
    public class MakeAppointmentRequest
    {
        public string EmployeeName { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrive { get; set; }

        public int ServiceDuration { get; set; }

        public DateTime Start { get; set; }
    }
}