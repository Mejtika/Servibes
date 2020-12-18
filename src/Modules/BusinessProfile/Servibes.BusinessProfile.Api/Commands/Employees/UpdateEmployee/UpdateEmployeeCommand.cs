using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public EmployeeForUpdateDto EmployeeForUpdateDto { get; }

        public UpdateEmployeeCommand(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdateDto)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            EmployeeForUpdateDto = employeeForUpdateDto;
        }
    }
}
