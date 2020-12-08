using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompany
{
    public class GetCompanyQuery : IRequest<CompanyDto>
    {
        public Guid CompanyId { get; set; }
    }
}
