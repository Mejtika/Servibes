using MediatR;
using Servibes.Availability.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.Availability.Application.Companies.ChangeOpeningHours
{
    public class ChangeOpeningHoursCommand : IRequest
    {
        public Guid CompanyId { get; }
        public List<HoursRangeDto> OpeningHours { get; }
        public bool AdjustEmployeeWorkingHours { get; }

        public ChangeOpeningHoursCommand(Guid companyId, List<HoursRangeDto> openingHours, bool adjustEmployeeWorkingHours)
        {
            this.CompanyId = companyId;
            this.OpeningHours = openingHours;
            this.AdjustEmployeeWorkingHours = adjustEmployeeWorkingHours;
        }
    }
}
