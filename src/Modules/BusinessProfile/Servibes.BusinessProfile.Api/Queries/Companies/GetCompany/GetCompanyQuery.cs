﻿using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompany
{
    public class GetCompanyQuery : IRequest<CompanyDetailsDto>
    {
        public Guid CompanyId { get; }

        public GetCompanyQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
