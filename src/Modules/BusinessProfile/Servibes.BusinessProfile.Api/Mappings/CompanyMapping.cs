using AutoMapper;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Queries.Company;

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
