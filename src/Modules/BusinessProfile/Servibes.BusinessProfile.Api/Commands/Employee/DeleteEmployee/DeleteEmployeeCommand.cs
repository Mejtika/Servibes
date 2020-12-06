using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
