using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public EmployeeDto EmployeeDto { get; set; }
    }
}
