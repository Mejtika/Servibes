using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Queries.Company.GetCompany
{
    public class GetCompanyQuery : IRequest<CompanyDto>
    {
        public Guid CompanyId { get; set; }
    }
}
