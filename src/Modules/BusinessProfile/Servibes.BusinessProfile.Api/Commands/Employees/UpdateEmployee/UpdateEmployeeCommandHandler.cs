using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly BusinessProfileContext _context;

        public UpdateEmployeeCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .SingleOrDefaultAsync(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId, cancellationToken);

            if (employee == null)
            {
                throw new AppException($"Employee {request.EmployeeId} or company {request.CompanyId} not found.");
            }

            employee.FirstName = request.EmployeeForUpdateDto.FirstName;
            employee.LastName = request.EmployeeForUpdateDto.LastName;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
