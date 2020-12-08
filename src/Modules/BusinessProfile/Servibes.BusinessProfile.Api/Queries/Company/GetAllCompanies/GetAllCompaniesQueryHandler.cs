using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Company.GetAllCompanies
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyDto>>
    {
        private readonly BusinessProfileContext context;
        private readonly IMapper mapper;

        public GetAllCompaniesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<IEnumerable<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = context.Companies.AsQueryable();

            if (!String.IsNullOrEmpty(request.Category))
                companies = companies.Where(c => c.Category == request.Category);

            return Task.FromResult(mapper.Map<IEnumerable<CompanyDto>>(companies.ToList()));
        }
    }
}
