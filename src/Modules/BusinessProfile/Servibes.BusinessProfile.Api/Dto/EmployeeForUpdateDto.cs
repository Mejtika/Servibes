using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Dto
{
    public class EmployeeForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<WorkingHoursDto> WorkingHours { get; set; }
    }
}
