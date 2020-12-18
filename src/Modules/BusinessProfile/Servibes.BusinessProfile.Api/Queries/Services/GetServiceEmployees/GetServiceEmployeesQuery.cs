using MediatR;
using Servibes.BusinessProfile.Api.Queries.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Services.GetServiceEmployees
{
    public class GetServiceEmployeesQuery : IRequest<IEnumerable<CompanyEmployeeDto>>
    {
        public Guid ServiceId { get; }

        public GetServiceEmployeesQuery(Guid serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
