using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Events;
using Servibes.BusinessProfile.Api.Models;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Employees.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMessageBroker _messageBroker;

        public CreateEmployeeCommandHandler(BusinessProfileContext context, IMessageBroker messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
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
            var @event = new EmployeeAddedEvent(employee.EmployeeId, employee.CompanyId, $"{employee.FirstName} {employee.LastName}");
            await _messageBroker.PublishAsync(new[] {@event});
            return employee.EmployeeId;
        }
    }
}
