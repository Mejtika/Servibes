using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
