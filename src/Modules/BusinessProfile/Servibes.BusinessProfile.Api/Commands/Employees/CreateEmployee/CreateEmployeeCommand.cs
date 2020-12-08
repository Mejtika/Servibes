using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public EmployeeDto EmployeeDto { get; set; }
    }
}
