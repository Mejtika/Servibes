using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.Appointments.Api;
using Servibes.Availability.Api.DTO;
using Servibes.Availability.Api.Requests;
using Servibes.Availability.Application.Companies.GetCompanyOpeningHours;
using Servibes.Availability.Application.Employees.GetEmployeeAvailableHours;
using Servibes.Availability.Application.Employees.GetEmployeeWorkingHours;
using Servibes.Availability.Application.Employees.MakeReservation;
using Servibes.Availability.Application.Employees.MakeTimeReservation;

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

        [HttpPost("{companyId}/employees/{employeeId}/appointments")]
        public async Task<IActionResult> MakeAppointment(Guid companyId, Guid employeeId, [FromBody] MakeAppointmentRequest request)
        {
            await _mediator.Send(new MakeReservationCommand(
                companyId,
                employeeId,
                request.ReserveeId,
                request.EmployeeName,
                request.ServiceName,
                request.ServicePrice,
                request.ServiceDuration,
                request.Start));

            return NoContent();
        }

        [HttpPost("{companyId}/employees/{employeeId}/timeReservations")]
        public async Task<IActionResult> MakeTimeReservation(Guid companyId, Guid employeeId, [FromBody] MakeTimeReservationRequest request)
        {
            await _mediator.Send(new MakeTimeReservationCommand(
                companyId,
                employeeId,
                request.Start,
                request.End));

            return NoContent();
        }
    }
}
