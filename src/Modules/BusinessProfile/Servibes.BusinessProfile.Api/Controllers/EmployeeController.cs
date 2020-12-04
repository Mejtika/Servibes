using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Dto;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/company")]
    public class EmployeeController : ControllerBase
    {
        private readonly BusinessProfileContext context;

        public EmployeeController(BusinessProfileContext context)
        {
            this.context = context;
        }

        [HttpGet("{companyId}/employee/{employeeId}")]
        public IActionResult GetEmployeeById(Guid companyId, Guid employeeId)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId && e.CompanyId == companyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {employeeId} doesnt exist.");

            return Ok(employee);
        }

        [HttpPost("{companyId}/employee")]
        public IActionResult CreateEmployee([FromBody]EmployeeDto employeeDto, Guid companyId)
        {
            var company = context.Companies.FirstOrDefault(c => c.CompanyId == companyId);

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            Employee employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                CompanyId = companyId,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                //WorkingHours = company.OpeningHours
            };

            context.Employees.Add(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { employee.EmployeeId });
        }

        [HttpPut("{companyId}/employee/{employeeId}")]
        public IActionResult UpdateEmployee([FromBody]EmployeeForUpdateDto employeeDto, Guid companyId, Guid employeeId)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {employeeId} doesn't exist.");

            var company = context.Companies.FirstOrDefault(c => c.CompanyId == companyId);

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesn't exist.");

            employee.CompanyId = companyId;
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            //employee.WorkingHours = WeekHoursRangeFactory.Create(employeeDto.WorkingHours);

            context.Employees.Add(employee);
            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{companyId}/employee/{employeeId}")]
        public IActionResult DeleteEmployee(Guid companyId, Guid employeeId)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId && e.CompanyId == companyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {employeeId} doesn't exist.");

            context.Employees.Remove(employee);

            return NoContent();
        }
    }
}
