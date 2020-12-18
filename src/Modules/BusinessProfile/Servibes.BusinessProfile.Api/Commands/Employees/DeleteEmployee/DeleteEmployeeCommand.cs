using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DeleteEmployeeCommand(Guid companyId, Guid employeeId)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
        }
    }
}
