using MediatR;
using Servibes.Availability.Application.Shared;
using System;
using System.Collections.Generic;

namespace Servibes.Availability.Application.Events.Companies
{
    public class CompanyAvailabilityChangedEvent : INotification
    {
        public List<HoursRangeDto> OpeningHoursDto { get; }
        public Guid CompanyId { get; set; }

        public CompanyAvailabilityChangedEvent(List<HoursRangeDto> openingHoursDto, Guid companyId)
        {
            OpeningHoursDto = openingHoursDto;
            CompanyId = companyId;
        }
    }
}