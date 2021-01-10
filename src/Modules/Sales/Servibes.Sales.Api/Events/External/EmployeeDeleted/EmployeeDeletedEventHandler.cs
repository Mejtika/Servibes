using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.Shared.Exceptions;

namespace Servibes.Sales.Api.Events.External.EmployeeDeleted
{
    public class EmployeeDeletedEventHandler : INotificationHandler<EmployeeDeletedEvent>
    {
        private readonly SalesContext _context;

        public EmployeeDeletedEventHandler(SalesContext context)
        {
            _context = context;
        }

        public async Task Handle(EmployeeDeletedEvent notification, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .SingleOrDefaultAsync(e => e.EmployeeId == notification.EmployeeId && e.CompanyId == notification.CompanyId, cancellationToken);
            if (employee == null)
            {
                throw new AppException($"Employee {notification.EmployeeId} or company {notification.CompanyId} not found.");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}