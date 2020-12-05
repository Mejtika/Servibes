using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices
{
    public class GetCompanyServicesQueryHandler : IRequestHandler<GetCompanyServicesQuery, IEnumerable<CompanyServicesDto>>
    {
        private readonly BusinessProfileContext context;
        private readonly IMapper mapper;

        public GetCompanyServicesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<IEnumerable<CompanyServicesDto>> Handle(GetCompanyServicesQuery request, CancellationToken cancellationToken)
        {
            var services = context.Services.Where(s => s.CompanyId == request.CompanyId).ToList();

            if (services.Count() == 0)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt have any services.");

            return Task.FromResult(mapper.Map<IEnumerable<CompanyServicesDto>>(services));
        }
    }
}
