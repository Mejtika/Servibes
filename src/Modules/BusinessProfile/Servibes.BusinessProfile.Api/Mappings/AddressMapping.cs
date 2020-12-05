using AutoMapper;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Queries.Company;
using System;
using System.Collections.Generic;
using System.Text;

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
