using AutoMapper;
using Servibes.BusinessProfile.Api.Models;
using Servibes.BusinessProfile.Api.Queries.Companies;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            this.CreateMap<Company, CompanyDto>();
        }
    }
}
