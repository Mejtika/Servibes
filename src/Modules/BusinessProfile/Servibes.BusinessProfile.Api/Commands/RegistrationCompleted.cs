using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands
{
    public class RegistrationCompleted : INotification
    {
        public Guid CompanyId { get; }

        public List<HoursRangeDto> HoursRangeDtos { get; }

        public RegistrationCompleted(Guid companyId, List<HoursRangeDto> hoursRangeDtos)
        {
            CompanyId = companyId;
            HoursRangeDtos = hoursRangeDtos;
        }
    }
}


