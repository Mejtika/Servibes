using System;
using Microsoft.AspNetCore.Mvc;

namespace Servibes.Availability.Api
{
    [ApiController]
    [Route("api/companies")]
    public class AvailabilityController : ControllerBase
    {
        [HttpPost("{companyId}/employees/{employeeId}/workingHours")]
        public IActionResult ChangeWorkingHours([FromBody] EmployeeWorkingHoursDto employeeWorkingHoursDto, Guid companyId, Guid employeeId)
        {

            return Ok();
        }

        [HttpPost("{companyId}/openingHours")]
        public IActionResult ChangeOpeningHours([FromBody] CompanyOpeningHoursDto companyOpeningHours, Guid companyId)
        {

            return Ok();
        }
    }
}
