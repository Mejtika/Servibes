using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, CompanyServiceDto>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CompanyServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.SingleOrDefaultAsync(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId, cancellationToken);

            if (service == null)
            {
                throw new AppException($"Service with id {request.ServiceId} or company {request.CompanyId} not found.");
            }

            return _mapper.Map<CompanyServiceDto>(service);
        }
    }
}
