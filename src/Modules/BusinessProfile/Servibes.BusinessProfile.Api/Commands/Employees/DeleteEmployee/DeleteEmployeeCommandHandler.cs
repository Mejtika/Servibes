using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly BusinessProfileContext _context;

        public DeleteEmployeeCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .SingleOrDefaultAsync(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId, cancellationToken);

            if (employee == null)
            {
                throw new AppException($"Employee {request.EmployeeId} or company {request.CompanyId} not found.");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
