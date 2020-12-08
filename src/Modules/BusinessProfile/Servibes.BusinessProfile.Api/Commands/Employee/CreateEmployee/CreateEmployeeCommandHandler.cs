using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly BusinessProfileContext _context;

        public CreateEmployeeCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var company = _context.Companies.FirstOrDefault(c => c.CompanyId == request.CompanyId);

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt exist.");

            Model.Employee employee = new Model.Employee()
            {
                EmployeeId = Guid.NewGuid(),
                CompanyId = request.CompanyId,
                FirstName = request.EmployeeDto.FirstName,
                LastName = request.EmployeeDto.LastName,
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Task.FromResult(employee.EmployeeId);
        }
    }
}
