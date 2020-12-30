using AutoMapper;
using Servibes.BusinessProfile.Api.Models;
using Servibes.BusinessProfile.Api.Queries.Services;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            this.CreateMap<Service, CompanyServiceDto>();
        }
    }
}
