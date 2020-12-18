using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Employees.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly BusinessProfileContext _context;

        public CreateEmployeeCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .SingleOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

            if (company == null)
            {
                throw new AppException($"Company with id {request.CompanyId} not found.");
            }

            var employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                CompanyId = request.CompanyId,
                FirstName = request.EmployeeDto.FirstName,
                LastName = request.EmployeeDto.LastName,
            };

            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return employee.EmployeeId;
        }
    }
}
