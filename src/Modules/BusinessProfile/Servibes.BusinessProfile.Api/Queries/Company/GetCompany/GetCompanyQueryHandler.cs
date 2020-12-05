using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Company.GetCompany
{
    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly BusinessProfileContext context;
        private readonly IMapper mapper;

        public GetCompanyQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var company = context.Companies.Where(c => c.CompanyId == request.CompanyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt exist.");

            return Task.FromResult(mapper.Map<CompanyDto>(company));
        }
    }
}
