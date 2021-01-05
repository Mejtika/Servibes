using System;

namespace Servibes.Sales.Api.Dtos
{
    public class AppointmentDto
    {
        public Guid AppointmentId { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}