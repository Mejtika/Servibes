using Servibes.Shared.Enumerations;

namespace Servibes.Availability.Application
{
    public class HoursRangeDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
