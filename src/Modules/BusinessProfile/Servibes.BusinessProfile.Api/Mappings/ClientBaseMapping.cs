using AutoMapper;
using Servibes.BusinessProfile.Api.Models.ClientBase;
using Servibes.BusinessProfile.Api.Queries.ClientBase;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class ClientBaseMapping : Profile
    {
        public ClientBaseMapping()
        {
            CreateMap<Client, ClientDto >()
                .ForMember(d => d.Name, d => d.MapFrom(x => x.Name));
        }
    }
}
