using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.Availability.Application.Companies.GetCompanyOpeningHours;

namespace Servibes.Availability.Api
{
    [ApiController]
    [Route("api/companies")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AvailabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

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

        [HttpGet("{companyId}/employees/{employeeId}/workingHours")]
        public IActionResult GetWorkingHours(Guid companyId, Guid employeeId)
        {

            return Ok();
        }

        [HttpGet("{companyId}/openingHours")]
        public async Task<IActionResult> GetOpeningHours(Guid companyId)
        {
            var response = await _mediator.Send(new GetCompanyOpeningHoursQuery(companyId));
            return Ok(response);
        }
    }
}
