﻿using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Servibes.Appointments.Application.Events.External.RegistrationCompleted
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
