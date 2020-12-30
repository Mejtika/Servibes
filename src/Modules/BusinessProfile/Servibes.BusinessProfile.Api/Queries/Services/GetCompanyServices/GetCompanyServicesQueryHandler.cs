using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQueryHandler : IRequestHandler<GetCompanyServicesQuery, IEnumerable<CompanyServiceDto>>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetCompanyServicesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CompanyServiceDto>> Handle(GetCompanyServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _context.Services.Where(s => s.CompanyId == request.CompanyId).ToListAsync();

            if (!services.Any())
            {
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt have any services.");
            }

            return _mapper.Map<IEnumerable<CompanyServiceDto>>(services);
        }
    }
}
