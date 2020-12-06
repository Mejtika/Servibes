using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Commands;
using Servibes.BusinessProfile.Api.Commands.Employee;
using Servibes.BusinessProfile.Api.Commands.Employee.CreateEmployee;
using Servibes.BusinessProfile.Api.Commands.Employee.DeleteEmployee;
using Servibes.BusinessProfile.Api.Commands.Employee.UpdateEmployee;
using Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees;
using Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{companyId}/employees/{employeeId}")]
        public IActionResult GetEmployeeById(Guid companyId, Guid employeeId)
        {
            var result = mediator.Send(new GetEmployeeByIdQuery()
            {
                CompanyId = companyId,
                EmployeeId = employeeId
            });

            return Ok(result);
        }

        [HttpGet("{companyId}/employees")]
        public IActionResult GetAllCompanyEmployees(Guid companyId)
        {
            var result = mediator.Send(new GetCompanyEmployeesQuery()
            {
                CompanyId = companyId
            });

            return Ok(result);
        }

        [HttpPost("{companyId}/employees")]
        public async Task<ActionResult> CreateEmployee([FromBody]EmployeeDto employeeDto, Guid companyId)
        {
            var result = await mediator.Send(new CreateEmployeeCommand()
            {
                CompanyId = companyId,
                EmployeeDto = employeeDto
            });

            return CreatedAtAction(nameof(GetEmployeeById), new { result });
        }

        [HttpPut("{companyId}/employees/{employeeId}")]
        public async Task<ActionResult> UpdateEmployee([FromBody] Commands.Employee.UpdateEmployee.EmployeeForUpdateDto employeeDto, Guid companyId, Guid employeeId)
        {
            await mediator.Send(new UpdateEmployeeCommand()
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
            await mediator.Send(new DeleteEmployeeCommand()
            {
                CompanyId = companyId,
                EmployeeId = employeeId
            });

            return NoContent();
        }
    }
}
