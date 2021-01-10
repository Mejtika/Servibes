using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Events.External.EmployeeDeleted
{
    public class EmployeeDeletedEvent : INotification
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public EmployeeDeletedEvent(Guid employeeId, Guid companyId)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}
