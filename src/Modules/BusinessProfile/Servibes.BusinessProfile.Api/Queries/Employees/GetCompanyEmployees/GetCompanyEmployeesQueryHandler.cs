using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public Task<IEnumerable<CompanyEmployeeDto>> Handle(GetCompanyEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _context.Employees.Where(e => e.CompanyId == request.CompanyId).ToList();

            if (!employees.Any())
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt have any employees.");

            return Task.FromResult(_mapper.Map<IEnumerable<CompanyEmployeeDto>>(employees));
        }
    }
}
