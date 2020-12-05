using AutoMapper;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Queries.Services.GetCompanyServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            this.CreateMap<Service, CompanyServicesDto>();
        }
    }
}
