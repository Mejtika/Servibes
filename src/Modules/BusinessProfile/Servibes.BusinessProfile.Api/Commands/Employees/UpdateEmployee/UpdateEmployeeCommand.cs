using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeForUpdateDto EmployeeForUpdateDto { get; set; }
    }
}
