using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Queries.Employees.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<CompanyEmployeeDto>
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public GetEmployeeByIdQuery(Guid companyId, Guid employeeId)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
        }
    }
}
