using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, CompanyServicesDto>
    {
        private readonly BusinessProfileContext context;
        private readonly IMapper mapper;

        public GetServiceByIdQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<CompanyServicesDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = context.Services.Where(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId);

            if (service == null)
                throw new ArgumentException($"Service with id {request.ServiceId} and company id {request.CompanyId} doesnt exist.");

            return Task.FromResult(mapper.Map<CompanyServicesDto>(service));
        }
    }
}
