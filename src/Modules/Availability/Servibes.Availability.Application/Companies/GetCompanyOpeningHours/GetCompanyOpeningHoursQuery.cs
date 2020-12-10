using System;
using System.Collections.Generic;
using MediatR;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Application.Companies.GetCompanyOpeningHours
{
    public class GetCompanyOpeningHoursQuery : IRequest<List<HoursRange>>
    {
        public Guid CompanyId { get; }

        public GetCompanyOpeningHoursQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
