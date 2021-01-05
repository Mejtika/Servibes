using System;
using System.Collections.Generic;
using MediatR;
using Servibes.Availability.Application.Shared;

namespace Servibes.Availability.Application.Events.External.RegistrationCompleted
{
    public class RegistrationCompletedEvent : INotification
    {
        public Guid CompanyId { get; }

        public Guid WalkInClientId { get; }

        public Guid OwnerId { get; }

        public List<HoursRangeDto> OpeningHoursDto { get; }

        public RegistrationCompletedEvent(
            Guid companyId,
            Guid walkInClientId,
            Guid ownerId,
            List<HoursRangeDto> openingHoursDto)
        {
            CompanyId = companyId;
            WalkInClientId = walkInClientId;
            OwnerId = ownerId;
            OpeningHoursDto = openingHoursDto;
        }
    }
}
