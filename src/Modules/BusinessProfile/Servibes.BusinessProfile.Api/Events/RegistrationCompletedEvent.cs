using System;
using System.Collections.Generic;
using MediatR;
using Servibes.BusinessProfile.Api.Commands;

namespace Servibes.BusinessProfile.Api.Events
{
    public class RegistrationCompletedEvent : INotification
    {
        public Guid CompanyId { get; }

        public Guid WalkInClientId { get; }

        public List<HoursRangeDto> OpeningHoursDto { get; }

        public RegistrationCompletedEvent(
            Guid companyId,
            Guid walkInClientId,
            List<HoursRangeDto> openingHoursDto)
        {
            CompanyId = companyId;
            WalkInClientId = walkInClientId;
            OpeningHoursDto = openingHoursDto;
        }
    }
}


