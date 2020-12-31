using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllClients;
using Servibes.BusinessProfile.Api.Queries.ClientBase.GetAllCompanyReviews;
using Servibes.BusinessProfile.Api.Queries.ClientBase.GetCompanyReviewsSummary;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class ClientBaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{companyId}/reviews")]
        public async Task<IActionResult> GetAllCompanyReviews(Guid companyId)
        {
            var result = await _mediator.Send(new GetAllCompanyReviewsQuery(companyId));
            return Ok(result);
        }

        [HttpGet("{companyId}/reviews/summary")]
        public async Task<IActionResult> GetCompanyReviewsSummary(Guid companyId)
        {
            var result = await _mediator.Send(new GetCompanyReviewsSummaryQuery(companyId));
            return Ok(result);
        }

        [HttpGet("{companyId}/clients")]
        public async Task<IActionResult> GetAllClient(Guid companyId)
        {
            var result = await _mediator.Send(new GetAllClientsQuery(companyId));
            return Ok(result);
        }
    }
}
