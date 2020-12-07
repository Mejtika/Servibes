using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<CompanyEmployeeDto>
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
