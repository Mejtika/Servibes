using MediatR;
using System.Collections.Generic;

namespace Servibes.BusinessProfile.Api.Queries.Company.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDto>>
    {
        public string Category { get; set; }
    }
}
