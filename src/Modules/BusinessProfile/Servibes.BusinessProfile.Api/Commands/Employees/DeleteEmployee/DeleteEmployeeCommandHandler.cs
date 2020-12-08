using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly BusinessProfileContext _context;

        public DeleteEmployeeCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {request.EmployeeId} and company id {request.CompanyId} doesn't exist.");

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Unit.Task;
        }
    }
}
