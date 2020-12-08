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
        private readonly BusinessProfileContext context;
        private readonly IMapper mapper;

        public GetCompanyEmployeesQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<IEnumerable<CompanyEmployeeDto>> Handle(GetCompanyEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = context.Employees.Where(e => e.CompanyId == request.CompanyId).ToList();

            if (employees.Count() == 0)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt have any employees.");

            return Task.FromResult(mapper.Map<IEnumerable<CompanyEmployeeDto>>(employees));
        }
    }
}
