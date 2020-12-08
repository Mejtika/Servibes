using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public Task<CompanyEmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {request.EmployeeId} and company id {request.CompanyId} doesnt exist.");

            return Task.FromResult(_mapper.Map<CompanyEmployeeDto>(employee));
        }
    }
}
