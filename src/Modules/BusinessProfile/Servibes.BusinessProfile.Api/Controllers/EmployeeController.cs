﻿using Microsoft.AspNetCore.Mvc;
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
            var employee = context.Employees.Where(e => e.EmployeeId == employeeId && e.CompanyId == companyId).FirstOrDefault();

            if (employee == null)
                throw new ArgumentException($"Employee with id {employeeId} doesnt exist.");

            return Ok(employee);
        }

        [HttpPost("{companyId}/employee")]
        public IActionResult CreateEmployee([FromBody]EmployeeDto employeeDto, Guid companyId)
        {
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            Employee employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                CompanyId = companyId,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                WorkingHours = company.OpeningHours.Select(oh => new WorkingHours()
                {
                    DayOfWeek = oh.DayOfWeek,
                    From = oh.From,
                    To = oh.To
                }).ToList()
            };

            context.Employees.Add(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { employee.EmployeeId });
        }
    }
}