using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices;
using static System.String;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompaniesWithSearch
{
    public class GetCompaniesWithSearchQueryHandler : IRequestHandler<GetCompaniesWithSearchQuery, PagedResult<IEnumerable<SearchedCompanyDto>>>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetCompaniesWithSearchQueryHandler(
            BusinessProfileContext context,
            IMediator mediator,
            IMapper mapper)
        {
            this._context = context;
            _mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<PagedResult<IEnumerable<SearchedCompanyDto>>> Handle(GetCompaniesWithSearchQuery request, CancellationToken cancellationToken)
        {
            var companies = _context.Companies.AsQueryable();
            if (!IsNullOrEmpty(request.Category))
            {
                companies = companies.Where(c => c.Category == request.Category);
            }

            var totalRecords = await companies.CountAsync(cancellationToken);

            if (PagingIsSet(request))
            {
                companies = companies
                    .OrderBy(on => on.CompanyName)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize);
            }


            var companiesDtos = _mapper.Map<List<SearchedCompanyDto>>(companies.ToList());
            foreach (var company in companiesDtos)
            {
                company.Services = (await _mediator.Send(new GetCompanyServicesQuery(company.CompanyId), cancellationToken)).ToList();
            }

            return new PagedResult<IEnumerable<SearchedCompanyDto>>
            {
                Results = companiesDtos,
                TotalRecords = totalRecords
            };
        }

        private bool PagingIsSet(GetCompaniesWithSearchQuery request)
        {
            return (request?.Page ?? 0) != 0 || (request?.PageSize ?? 0) != 0;
        }
    }
}
