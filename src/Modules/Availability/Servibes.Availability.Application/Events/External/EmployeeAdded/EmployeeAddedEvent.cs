using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Availability.Application.Events.External.EmployeeAdded
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