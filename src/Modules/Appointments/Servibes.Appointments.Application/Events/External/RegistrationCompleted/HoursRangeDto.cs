using System;

namespace Servibes.Appointments.Application.Events.External.RegistrationCompleted
{
    public class HoursRangeDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
