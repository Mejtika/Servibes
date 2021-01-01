using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Events
{
    public class EmployeeAddedEvent : INotification
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public string Name { get; set; }

        public EmployeeAddedEvent(
            Guid employeeId,
            Guid companyId, 
            string name)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
            Name = name;
        }
    }
}