﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Commands.Service;
using Servibes.BusinessProfile.Api.Commands.Service.CreateService;
using Servibes.BusinessProfile.Api.Commands.Service.DeleteService;
using Servibes.BusinessProfile.Api.Commands.Service.UpdateService;
using Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices;
using Servibes.BusinessProfile.Api.Queries.Services.GetServiceById;
using System;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class ServiceController : ControllerBase
    {
        private readonly BusinessProfileContext _context;
        private readonly IMediator _mediator;

        public ServiceController(BusinessProfileContext context, IMediator mediator)
        {
            this._context = context;
            this._mediator = mediator;
        }

        [HttpGet("{companyId}/services/{serviceId}")]
        public async Task<ActionResult> GetServiceById(Guid companyId, Guid serviceId)
        {
            var result = await _mediator.Send(new GetServiceByIdQuery()
            {
                CompanyId = companyId,
                ServiceId = serviceId
            });

            return Ok(result);
        }

        [HttpGet("{companyId}/services")]
        public async Task<ActionResult> GetAllCompanyServices(Guid companyId)
        {
            var result = await this._mediator.Send(new GetCompanyServicesQuery()
            {
                CompanyId = companyId
            });

            return Ok(result);
        }

        [HttpPost("{companyId}/services")]
        public async Task<ActionResult> CreateService([FromBody]ServiceDto serviceDto, Guid companyId)
        {
            var result = await _mediator.Send(new CreateServiceCommand()
            {
                CompanyId = companyId,
                ServicDto = serviceDto
            });

            return CreatedAtAction(nameof(GetServiceById), new { result });
        }
        
        [HttpPut("{companyId}/services/{serviceId}")]
        public async Task<ActionResult> UpdateService([FromBody] ServiceDto serviceDto, Guid companyId, Guid serviceId)
        {
            await _mediator.Send(new UpdateServiceCommand()
            {
                CompanyId = companyId,
                ServiceId = serviceId,
                ServiceDto = serviceDto
            });

            return NoContent();
        }

        [HttpDelete("{companyId}/services/{serviceId}")]
        public async Task<ActionResult> DeleteService(Guid companyId, Guid serviceId)
        {
            await _mediator.Send(new DeleteServiceCommand()
            {
                CompanyId = companyId,
                ServiceId = serviceId
            });

            return NoContent();
        }
    }
}
