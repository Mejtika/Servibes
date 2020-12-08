﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetAllCompanies
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyDto>>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Task<IEnumerable<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = _context.Companies.AsQueryable();

            if (!String.IsNullOrEmpty(request.Category))
                companies = companies.Where(c => c.Category == request.Category);

            return Task.FromResult(_mapper.Map<IEnumerable<CompanyDto>>(companies.ToList()));
        }
    }
}