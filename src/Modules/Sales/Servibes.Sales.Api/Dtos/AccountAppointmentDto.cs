using System;

namespace Servibes.Sales.Api.Dtos
{
    public class AccountAppointmentDto
    {
        public Guid AppointmentId { get; set; }

        public decimal Price { get; set; }
    }
}
