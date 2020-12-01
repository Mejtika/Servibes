using System.Collections.Generic;
using System.Text;
using Servibes.BusinessProfile.Api.Model.Enumerations;

namespace Servibes.BusinessProfile.Api.Models
{
    public class OpenHoursDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsOpen { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
    }
}
