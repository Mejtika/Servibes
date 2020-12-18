using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompany
{
    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDto>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken: cancellationToken);

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesn't exist.");

            return _mapper.Map<CompanyDto>(company);
        }
    }
}
