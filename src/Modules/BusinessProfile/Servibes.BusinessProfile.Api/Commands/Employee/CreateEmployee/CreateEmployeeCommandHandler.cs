using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly BusinessProfileContext context;

        public CreateEmployeeCommandHandler(BusinessProfileContext context)
        {
            this.context = context;
        }

        public Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var company = context.Companies.FirstOrDefault(c => c.CompanyId == request.CompanyId);

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt exist.");

            Model.Employee employee = new Model.Employee()
            {
                EmployeeId = Guid.NewGuid(),
                CompanyId = request.CompanyId,
                FirstName = request.EmployeeDto.FirstName,
                LastName = request.EmployeeDto.LastName,
            };

            context.Employees.Add(employee);
            context.SaveChanges();

            return Task.FromResult(employee.EmployeeId);
        }
    }
}
