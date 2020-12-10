using System.Collections.Generic;

namespace Servibes.Availability.Api
{
    public class CompanyOpeningHoursDto
    {
        public List<HoursRangeDto> OpeningHours { get; set; }
        public bool AdjustEmployeeWorkingHours { get; set; }
    }
}