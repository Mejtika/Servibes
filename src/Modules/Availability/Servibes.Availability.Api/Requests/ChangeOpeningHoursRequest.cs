using System.Collections.Generic;
using Servibes.Availability.Application.Shared;

namespace Servibes.Availability.Api.Requests
{
    public class ChangeOpeningHoursRequest
    {
        public List<HoursRangeDto> OpeningHours { get; set; }
        public bool AdjustEmployeeWorkingHours { get; set; }
    }
}