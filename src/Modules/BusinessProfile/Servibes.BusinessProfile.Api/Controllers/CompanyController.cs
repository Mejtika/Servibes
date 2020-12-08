using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using Servibes.BusinessProfile.Api.Queries.Company.GetCompany;
using Servibes.BusinessProfile.Api.Queries.Company.GetAllCompanies;
using Servibes.BusinessProfile.Api.Commands.Company;
using Servibes.BusinessProfile.Api.Commands.Company.CreateCompany;
using System.Threading.Tasks;
using Servibes.BusinessProfile.Api.Commands.Company.UpdateCompany;
using Servibes.BusinessProfile.Api.Commands.Company.DeleteCompany;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator mediator;

        public CompanyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult> GetCompanyById(Guid companyId)
        {
            var result = await mediator.Send(new GetCompanyQuery()
            {
                CompanyId = companyId
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCompanies(string category = "")
        {
            var result = await mediator.Send(new GetAllCompaniesQuery()
            {
                Category = category
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody]CompanyDto companyDto)
        {
            var result = await mediator.Send(new CreateCompanyCommand()
            {
                CompanyDto = companyDto
            });

            return CreatedAtAction(nameof(GetCompanyById), new { result });
        }

        [HttpPut("{companyId}")]
        public async Task<ActionResult> UpdateCompanyInfo([FromBody]UpdateCompanyDto updateCompanyDto, Guid companyId)
        {
            await mediator.Send(new UpdateCompanyCommand()
            {
                CompanyId = companyId,
                UpdateCompanyDto = updateCompanyDto
            });

            return NoContent();
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult> DeleteCompany(Guid companyId)
        {
            await mediator.Send(new DeleteCompanyCommand()
            {
                CompanyId = companyId
            });

            return NoContent();
        }
    }
}
