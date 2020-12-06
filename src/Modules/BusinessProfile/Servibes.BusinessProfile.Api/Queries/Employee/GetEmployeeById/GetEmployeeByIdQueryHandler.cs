using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, CompanyEmployeesDto>
    {
        private readonly BusinessProfileContext context;
        private readonly IMapper mapper;

        public GetEmployeeByIdQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<CompanyEmployeesDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = context.Employees.Where(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {request.EmployeeId} and company id {request.CompanyId} doesnt exist.");

            return Task.FromResult(mapper.Map<CompanyEmployeesDto>(employee));
        }
    }
}
