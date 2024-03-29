﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetOwnerCompany
{
    public class GetOwnerCompanyQueryHandler : IRequestHandler<GetOwnerCompanyQuery,CompanyDetailsDto>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public GetOwnerCompanyQueryHandler(
            BusinessProfileContext context,
            IMapper mapper,
            IHttpContextAccessor accessor)
        {
            this._context = context;
            this._mapper = mapper;
            _accessor = accessor;
        }

        public async Task<CompanyDetailsDto> Handle(GetOwnerCompanyQuery request, CancellationToken cancellationToken)
        {
            var ownerId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty);
            var company = await _context.Companies.SingleOrDefaultAsync(c => c.OwnerId == ownerId, cancellationToken);
            return _mapper.Map<CompanyDetailsDto>(company);
        }
    }
}