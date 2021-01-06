using System;

namespace Servibes.Availability.Api
{
    public class GiveTimeOffRequest
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}