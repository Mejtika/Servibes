using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, CompanyServicesDto>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Task<CompanyServicesDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId);

            if (service == null)
                throw new ArgumentException($"Service with id {request.ServiceId} and company id {request.CompanyId} doesnt exist.");

            return Task.FromResult(_mapper.Map<CompanyServicesDto>(service));
        }
    }
}
