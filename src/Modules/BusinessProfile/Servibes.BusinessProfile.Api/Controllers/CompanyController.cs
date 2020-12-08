using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using System.Threading.Tasks;
using Servibes.BusinessProfile.Api.Commands.Companies;
using Servibes.BusinessProfile.Api.Commands.Companies.CreateCompany;
using Servibes.BusinessProfile.Api.Commands.Companies.DeleteCompany;
using Servibes.BusinessProfile.Api.Commands.Companies.UpdateCompany;
using Servibes.BusinessProfile.Api.Queries.Companies.GetAllCompanies;
using Servibes.BusinessProfile.Api.Queries.Companies.GetCompany;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult> GetCompanyById(Guid companyId)
        {
            var result = await _mediator.Send(new GetCompanyQuery()
            {
                CompanyId = companyId
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCompanies(string category = "")
        {
            var result = await _mediator.Send(new GetAllCompaniesQuery()
            {
                Category = category
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody]CompanyDto companyDto)
        {
            var result = await _mediator.Send(new CreateCompanyCommand()
            {
                CompanyDto = companyDto
            });

            return CreatedAtAction(nameof(GetCompanyById), new { result });
        }

        [HttpPut("{companyId}")]
        public async Task<ActionResult> UpdateCompanyInfo([FromBody]UpdateCompanyDto updateCompanyDto, Guid companyId)
        {
            await _mediator.Send(new UpdateCompanyCommand()
            {
                CompanyId = companyId,
                UpdateCompanyDto = updateCompanyDto
            });

            return NoContent();
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult> DeleteCompany(Guid companyId)
        {
            await _mediator.Send(new DeleteCompanyCommand()
            {
                CompanyId = companyId
            });

            return NoContent();
        }
    }
}
