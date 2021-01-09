using System;

namespace Servibes.Appointments.Api
{
    public class TimeReservationDto
    {
        public Guid TimeReservationId { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Status { get; set; }
    }
}