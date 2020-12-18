using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; }

        public EmployeeDto EmployeeDto { get; }

        public CreateEmployeeCommand(Guid companyId, EmployeeDto employeeDto)
        {
            CompanyId = companyId;
            EmployeeDto = employeeDto;
        }
    }
}
