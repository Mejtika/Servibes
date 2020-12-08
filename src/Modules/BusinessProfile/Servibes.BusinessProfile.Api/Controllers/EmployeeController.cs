using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Commands;
using Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees;
using Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById;
using System;
using System.Threading.Tasks;
using Servibes.BusinessProfile.Api.Commands.Employees.CreateEmployee;
using Servibes.BusinessProfile.Api.Commands.Employees.DeleteEmployee;
using Servibes.BusinessProfile.Api.Commands.Employees.UpdateEmployee;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{companyId}/employees/{employeeId}")]
        public async Task<ActionResult> GetEmployeeById(Guid companyId, Guid employeeId)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery()
            {
                CompanyId = companyId,
                EmployeeId = employeeId
            });

            return Ok(result);
        }

        [HttpGet("{companyId}/employees")]
        public async Task<ActionResult> GetAllCompanyEmployees(Guid companyId)
        {
            var result = await _mediator.Send(new GetCompanyEmployeesQuery()
            {
                CompanyId = companyId
            });

            return Ok(result);
        }

        [HttpPost("{companyId}/employees")]
        public async Task<ActionResult> CreateEmployee([FromBody]EmployeeDto employeeDto, Guid companyId)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand()
            {
                CompanyId = companyId,
                EmployeeDto = employeeDto
            });

            return CreatedAtAction(nameof(GetEmployeeById), new { result });
        }

        [HttpPut("{companyId}/employees/{employeeId}")]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeForUpdateDto employeeDto, Guid companyId, Guid employeeId)
        {
            await _mediator.Send(new UpdateEmployeeCommand()
            {
                CompanyId = companyId,
                EmployeeId = employeeId,
                EmployeeForUpdateDto = employeeDto
            });

            return NoContent();
        }

        [HttpDelete("{companyId}/employees/{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(Guid companyId, Guid employeeId)
        {
            await _mediator.Send(new DeleteEmployeeCommand()
            {
                CompanyId = companyId,
                EmployeeId = employeeId
            });

            return NoContent();
        }
    }
}
