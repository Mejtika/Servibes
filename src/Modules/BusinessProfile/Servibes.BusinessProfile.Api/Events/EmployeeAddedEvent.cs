using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Events
{
    public class EmployeeAddedEvent : INotification
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public EmployeeAddedEvent(Guid employeeId, Guid companyId)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}