using AutoMapper;
using Servibes.BusinessProfile.Api.Models;
using Servibes.BusinessProfile.Api.Queries.Companies;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class AddressMapping : Profile
    {
        public AddressMapping()
        {
            this.CreateMap<Address, CompanyAddressDto>();
        }
    }
}
