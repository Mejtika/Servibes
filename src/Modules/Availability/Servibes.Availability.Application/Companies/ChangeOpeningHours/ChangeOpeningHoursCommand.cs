using MediatR;
using Servibes.Availability.Application.Shared;
using System;
using System.Collections.Generic;

namespace Servibes.Availability.Application.Companies.ChangeOpeningHours
{
    public class ChangeOpeningHoursCommand : IRequest
    {
        public Guid CompanyId { get; }
        public List<HoursRangeDto> OpeningHours { get; }
        public bool AdjustEmployeeWorkingHours { get; }

        public ChangeOpeningHoursCommand(
            Guid companyId,
            List<HoursRangeDto> openingHours,
            bool adjustEmployeeWorkingHours)
        {
            CompanyId = companyId;
            OpeningHours = openingHours;
            AdjustEmployeeWorkingHours = adjustEmployeeWorkingHours;
        }
    }
}
