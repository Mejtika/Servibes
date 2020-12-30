using AutoMapper;
using Servibes.BusinessProfile.Api.Models;
using Servibes.BusinessProfile.Api.Queries.Companies;
using Servibes.BusinessProfile.Api.Queries.Companies.GetAllCompanies;
using Servibes.BusinessProfile.Api.Queries.Companies.GetCompaniesWithSearch;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            this.CreateMap<Company, CompanyDto>();
            this.CreateMap<Company, CompanyDetailsDto>();
            this.CreateMap<Company, SearchedCompanyDto>();
        }
    }
}