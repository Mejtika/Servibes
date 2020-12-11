using System;

namespace Servibes.Availability.Api.Requests
{
    public class EmployeeDayAvailabilityRequest
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
    }
}