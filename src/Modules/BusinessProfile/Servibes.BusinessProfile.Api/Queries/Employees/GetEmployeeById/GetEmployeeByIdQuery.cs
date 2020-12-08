using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<CompanyEmployeeDto>
    {
        public Guid CompanyId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
