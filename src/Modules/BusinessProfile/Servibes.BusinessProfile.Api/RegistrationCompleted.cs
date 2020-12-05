using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Servibes.BusinessProfile.Api.Models;

namespace Servibes.BusinessProfile.Api
{
    public class RegistrationCompleted : INotification
    {
        public RegistrationCompleted(List<HoursRangeDto> hoursRangeDtos, List<Guid> employeeId, Guid companyId)
        {
            HoursRangeDtos = hoursRangeDtos;
            EmployeeId = employeeId;
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
        public List<Guid> EmployeeId { get; }
        public List<HoursRangeDto> HoursRangeDtos { get; }
    }
}
