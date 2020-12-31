using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using System.Threading.Tasks;
using Servibes.BusinessProfile.Api.Commands.Companies.CreateCompany;
using Servibes.BusinessProfile.Api.Commands.Companies.DeleteCompany;
using Servibes.BusinessProfile.Api.Commands.Companies.UpdateCompany;
using Servibes.BusinessProfile.Api.Queries.Companies.GetAllCompanies;
using Servibes.BusinessProfile.Api.Queries.Companies.GetCompaniesWithSearch;
using Servibes.BusinessProfile.Api.Queries.Companies.GetCompany;
using Servibes.BusinessProfile.Api.Queries.Companies.GetOwnerCompany;
using Servibes.BusinessProfile.Api.Queries.Companies.GetAllCategories;
using CompanyDto = Servibes.BusinessProfile.Api.Commands.Companies.CompanyDto;

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

        [HttpGet("owner")]
        public async Task<ActionResult> GetOwnerCompany()
        {
            var result = await _mediator.Send(new GetOwnerCompanyQuery());
            return Ok(result);
        }

        [HttpGet("owner/exists")]
        public async Task<ActionResult> IsOwnerCompanyExists()
        {
            var result = await _mediator.Send(new GetOwnerCompanyQuery()) == null ? false : true;

            return Ok(result);
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult> GetCompanyById(Guid companyId)
        {
            var result = await _mediator.Send(new GetCompanyQuery(companyId));
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCompanies()
        {
            var result = await _mediator.Send(new GetAllCompaniesQuery());
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetCompaniesWithSearch(int page, int pageSize, string category = "")
        {
            var result = await _mediator.Send(new GetCompaniesWithSearchQuery(page, pageSize, category));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody] CompanyDto companyDto)
        {
            var result = await _mediator.Send(new CreateCompanyCommand(companyDto));
            return CreatedAtAction(nameof(GetCompanyById), new { result });
        }

        [HttpPut("{companyId}")]
        public async Task<ActionResult> UpdateCompanyInfo([FromBody] UpdateCompanyDto updateCompanyDto, Guid companyId)
        {
            await _mediator.Send(new UpdateCompanyCommand(companyId, updateCompanyDto));
            return NoContent();
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult> DeleteCompany(Guid companyId)
        {
            await _mediator.Send(new DeleteCompanyCommand(companyId));
            return NoContent();
        }

        [HttpGet("categories")]
        public async Task<ActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            return Ok(result);
        }
    }
}
