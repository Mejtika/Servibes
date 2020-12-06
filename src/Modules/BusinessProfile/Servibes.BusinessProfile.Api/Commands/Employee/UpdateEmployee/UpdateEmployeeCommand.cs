using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeForUpdateDto EmployeeForUpdateDto { get; set; }
    }
}
