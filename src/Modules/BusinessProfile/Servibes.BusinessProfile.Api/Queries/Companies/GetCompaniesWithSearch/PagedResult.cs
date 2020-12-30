namespace Servibes.BusinessProfile.Api.Queries.Companies.GetCompaniesWithSearch
{
    public class PagedResult<T>
    {
        public T Results { get; set; }
        public int TotalRecords { get; set; }
    }
}