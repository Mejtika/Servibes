using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands
{
    public class RegistrationCompletedEvent : INotification
    {
        public Guid CompanyId { get; }

        public List<HoursRangeDto> OpeningHoursDto { get; }

        public RegistrationCompletedEvent(Guid companyId, List<HoursRangeDto> openingHoursDto)
        {
            CompanyId = companyId;
            OpeningHoursDto = openingHoursDto;
        }
    }
}


