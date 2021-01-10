using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Shared.Exceptions;

namespace Servibes.Sales.Api.Events.External.EmployeeDeleted
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
