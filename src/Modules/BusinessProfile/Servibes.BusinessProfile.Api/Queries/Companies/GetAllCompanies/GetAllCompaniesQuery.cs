using System.Collections.Generic;
using MediatR;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDto>>
    {
        public string Category { get; }

        public GetAllCompaniesQuery(string category)
        {
            Category = category;
        }
    }
}
