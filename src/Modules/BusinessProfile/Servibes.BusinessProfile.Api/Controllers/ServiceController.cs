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
    public class ServiceController : ControllerBase
    {
        private readonly BusinessProfileContext context;

        public ServiceController(BusinessProfileContext context)
        {
            this.context = context;
        }

        [HttpGet("{companyId}/service/{serviceId}")]
        public ActionResult<Service> GetServiceById(Guid companyId, Guid serviceId)
        {
            var service = context.Services.FirstOrDefault(s => s.ServiceId == serviceId && s.CompanyId == companyId);

            if (service == null)
                throw new ArgumentException($"Service with id {serviceId} doesnt exist for company id {companyId}.");

            return Ok(service);
        }

        [HttpPost("{companyId}/service")]
        public IActionResult CreateService([FromBody]ServiceDto serviceDto, Guid companyId)
        {
            var companyEmployees = context.Employees.Where(e => e.CompanyId == companyId);

            if (companyEmployees.Count() == 0) //Should never happend
                throw new ArgumentException($"Company with id {companyId} doesnt have any employees.");

            Service service = new Service()
            {
                ServiceId = Guid.NewGuid(),
                CompanyId = companyId,
                Description = serviceDto.Description,
                Duration = serviceDto.Duration,
                Price = serviceDto.Price,
                ServiceName = serviceDto.ServiceName
            };

            serviceDto.Performers.Where(s => s.IsActive).ToList().ForEach(e =>
            {
                service.Performers.Add(new Performer()
                {
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });


            context.Services.Add(service);

            return CreatedAtAction(nameof(GetServiceById), new { service.ServiceId });
        }
        
        [HttpPut("{companyId}/service/{serviceId}")]
        public IActionResult UpdateService([FromBody] ServiceDto serviceDto, Guid companyId, Guid serviceId)
        {
            var service = context.Services.FirstOrDefault(s => s.ServiceId == serviceId && s.CompanyId == companyId);

            if (service == null)
                throw new ArgumentException($"Service with id {serviceId} doesnt exist.");

            var companyEmployees = context.Employees.Where(e => e.CompanyId == service.CompanyId).ToList();

            if (companyEmployees.Count == 0)
                throw new ArgumentException($"Company with id {service.CompanyId} doesnt have any employees.");

            service.ServiceName = serviceDto.ServiceName;
            service.Price = service.Price;
            service.Duration = service.Duration;
            service.Description = service.Description;

            service.Performers.Clear();
            serviceDto.Performers.Where(s => s.IsActive).ToList().ForEach(e =>
            {
                service.Performers.Add(new Performer()
                {
                    PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName).EmployeeId
                });
            });

            context.Services.Update(service);

            return NoContent();
        }

        [HttpDelete("{companyId}/service/{serviceId}")]
        public IActionResult DeleteService(Guid companyId, Guid serviceId)
        {
            var service = context.Services.Where(s => s.ServiceId == serviceId && s.CompanyId == companyId).FirstOrDefault();

            if (service == null)
                throw new ArgumentException($"Service with id {serviceId} doesnt exist.");

            context.Services.Remove(service);

            return NoContent();
        }
    }
}
