using System.Collections.Generic;
using Servibes.Availability.Application.Shared;

namespace Servibes.Availability.Api.Requests
{
    public class ChangeWorkingHoursRequest
    {
        public List<HoursRangeDto> WorkingHours { get; set; }
    }
}