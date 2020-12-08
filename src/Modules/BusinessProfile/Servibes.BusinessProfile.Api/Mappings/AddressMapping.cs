using AutoMapper;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Queries.Company;

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
