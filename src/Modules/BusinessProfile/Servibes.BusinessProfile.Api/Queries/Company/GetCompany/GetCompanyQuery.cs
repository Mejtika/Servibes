using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Company.GetCompany
{
    public class GetCompanyQuery : IRequest<CompanyDto>
    {
        public Guid CompanyId { get; set; }
    }
}
