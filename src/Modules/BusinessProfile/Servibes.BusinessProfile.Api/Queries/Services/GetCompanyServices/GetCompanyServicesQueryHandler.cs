using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQueryHandler : IRequestHandler<GetCompanyServicesQuery, IEnumerable<CompanyServicesDto>>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetCompanyServicesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Task<IEnumerable<CompanyServicesDto>> Handle(GetCompanyServicesQuery request, CancellationToken cancellationToken)
        {
            var services = _context.Services.Where(s => s.CompanyId == request.CompanyId).ToList();

            if (!services.Any())
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt have any services.");

            return Task.FromResult(_mapper.Map<IEnumerable<CompanyServicesDto>>(services));
        }
    }
}
