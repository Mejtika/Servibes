using System;

namespace Servibes.Sales.Api
{
    public class PaidAppointmentsDto
    {
        public Guid AppointmentId { get; set; }

        public decimal Price { get; set; }
    }
}
