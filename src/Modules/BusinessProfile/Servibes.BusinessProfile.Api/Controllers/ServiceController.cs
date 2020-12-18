using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices;
using Servibes.BusinessProfile.Api.Queries.Services.GetServiceById;
using System;
using System.Threading.Tasks;
using Servibes.BusinessProfile.Api.Commands.Services;
using Servibes.BusinessProfile.Api.Commands.Services.CreateService;
using Servibes.BusinessProfile.Api.Commands.Services.DeleteService;
using Servibes.BusinessProfile.Api.Commands.Services.UpdateService;
using Servibes.BusinessProfile.Api.Queries.Services.GetServiceEmployees;

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
            var result = await _mediator.Send(new GetServiceByIdQuery(companyId, serviceId));
            return Ok(result);
        }

        [HttpGet("{companyId}/services")]
        public async Task<ActionResult> GetAllCompanyServices(Guid companyId)
        {
            var result = await _mediator.Send(new GetCompanyServicesQuery(companyId));
            return Ok(result);
        }

        [HttpGet("{companyId}/services/{serviceId}/employees")]
        public async Task<ActionResult> GetServiceEmployees(Guid companyId, Guid serviceId)
        {
            var result = await _mediator.Send(new GetServiceEmployeesQuery(serviceId));
            return Ok(result);
        }

        [HttpPost("{companyId}/services")]
        public async Task<ActionResult> CreateService(Guid companyId, [FromBody] ServiceDto serviceDto)
        {
            var result = await _mediator.Send(new CreateServiceCommand(companyId, serviceDto));
            return CreatedAtAction(nameof(GetServiceById), new { result });
        }
        
        [HttpPut("{companyId}/services/{serviceId}")]
        public async Task<ActionResult> UpdateService(Guid companyId, Guid serviceId, [FromBody] ServiceDto serviceDto)
        {
            await _mediator.Send(new UpdateServiceCommand(companyId, serviceId, serviceDto));
            return NoContent();
        }

        [HttpDelete("{companyId}/services/{serviceId}")]
        public async Task<ActionResult> DeleteService(Guid companyId, Guid serviceId)
        {
            await _mediator.Send(new DeleteServiceCommand(companyId, serviceId));
            return NoContent();
        }
    }
}
