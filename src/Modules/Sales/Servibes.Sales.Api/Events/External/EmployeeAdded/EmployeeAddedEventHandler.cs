using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Sales.Api.Models;

namespace Servibes.Sales.Api.Events.External.EmployeeAdded
{
    public class EmployeeAddedEventHandler : INotificationHandler<EmployeeAddedEvent>
    {
        private readonly SalesContext _context;

        public EmployeeAddedEventHandler(SalesContext context)
        {
            _context = context;
        }

        public async Task Handle(EmployeeAddedEvent notification, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                EmployeeId = notification.EmployeeId,
                CompanyId = notification.CompanyId,
                Name = notification.Name,
            };

            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}