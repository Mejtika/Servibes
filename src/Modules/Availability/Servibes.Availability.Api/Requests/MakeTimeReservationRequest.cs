using System;

namespace Servibes.Availability.Api.Requests
{
    public class MakeTimeReservationRequest
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}