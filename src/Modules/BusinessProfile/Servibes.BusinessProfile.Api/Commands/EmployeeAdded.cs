using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands
{
    public class EmployeeAdded : INotification
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public EmployeeAdded(Guid employeeId, Guid companyId)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}