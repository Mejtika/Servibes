using System;

namespace Servibes.Availability.Application.Employees.GetEmployeeAvailableHours
{
    public class WorkingHoursDto
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
