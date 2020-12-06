using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Company
{
    public class CompanyHoursRangeDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
