using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetCompanyEmployees
{
    public class GetCompanyEmployeesQueryHandler : IRequestHandler<GetCompanyEmployeesQuery, IEnumerable<CompanyEmployeeDto>>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetCompanyEmployeesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CompanyEmployeeDto>> Handle(GetCompanyEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees.Where(e => e.CompanyId == request.CompanyId).ToListAsync(cancellationToken);

            if (!employees.Any())
            {
                throw new AppException($"Company with id {request.CompanyId} doesn't have any employees.");
            }

            return _mapper.Map<IEnumerable<CompanyEmployeeDto>>(employees);
        }
    }
}
