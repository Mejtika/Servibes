using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.Availability.Api.Requests;
using Servibes.Availability.Application.Companies.GetCompanyOpeningHours;
using Servibes.Availability.Application.Employees.GetEmployeeAvailableHours;
using Servibes.Availability.Application.Employees.GetEmployeeWorkingHours;

namespace Servibes.Availability.Api
{
    [ApiController]
    [Route("api/companies")]
    public class BusinessAvailabilityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessAvailabilityController(IMediator mediator)
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
        public async Task<IActionResult> GetWorkingHours(Guid companyId, Guid employeeId)
        {
            var response = await _mediator.Send(new GetEmployeeWorkingHoursQuery(employeeId, companyId));
            return Ok(response);
        }

        [HttpGet("{companyId}/openingHours")]
        public async Task<IActionResult> GetOpeningHours(Guid companyId)
        {
            var response = await _mediator.Send(new GetCompanyOpeningHoursQuery(companyId));
            return Ok(response);
        }

        [HttpGet("{companyId}/employees/{employeeId}/availability")]
        public async Task<IActionResult> GetEmployeeDayAvailability(Guid companyId, Guid employeeId, [FromQuery]EmployeeDayAvailabilityRequest request)
        {
            var response = await _mediator.Send(new GetEmployeeAvailableHoursQuery(employeeId, companyId, request.Date, request.Duration));
            return Ok(response);
        }
    }
}
