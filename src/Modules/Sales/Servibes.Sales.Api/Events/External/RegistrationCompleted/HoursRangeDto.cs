using System;

namespace Servibes.Sales.Api.Events.External.RegistrationCompleted
{
    public class HoursRangeDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
