using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Availability.Application.Employees.GetEmployeeAvailableHours
{
    public class GetEmployeeAvailableHoursQuery : IRequest<List<AvailableHoursDto>>
    {
        public Guid EmployeeId { get; }

        public Guid CompanyId { get; }

        public DateTime Date { get; }

        public int Duration { get; }

        public GetEmployeeAvailableHoursQuery(Guid employeeId, Guid companyId, DateTime date, int duration)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
            Date = date;
            Duration = duration;
        }
    }
}
