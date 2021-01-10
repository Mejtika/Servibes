using System;
using MediatR;

namespace Servibes.Availability.Application.Employees.DeleteEmployee
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
