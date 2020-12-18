using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, CompanyEmployeeDto>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(BusinessProfileContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CompanyEmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .SingleOrDefaultAsync(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId, cancellationToken: cancellationToken);

            if (employee == null)
            {
                throw new ArgumentException($"Employee {request.EmployeeId} or company {request.CompanyId} not found.");
            }

            return _mapper.Map<CompanyEmployeeDto>(employee);
        }
    }
}
