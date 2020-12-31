using System.Collections.Generic;
using MediatR;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompaniesWithSearch
{
    public class GetCompaniesWithSearchQuery : IRequest<PagedResult<IEnumerable<SearchedCompanyDto>>>
    {
        public int Page { get; }

        public int PageSize { get; }

        public string Category { get; }

        public GetCompaniesWithSearchQuery(int page, int pageSize, string category)
        {
            Page = page;
            PageSize = pageSize;
            Category = category;
        }
    }
}
