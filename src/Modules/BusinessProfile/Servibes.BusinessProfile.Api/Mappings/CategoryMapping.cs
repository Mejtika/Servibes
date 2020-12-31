using AutoMapper;
using Servibes.BusinessProfile.Api.Models;
using Servibes.BusinessProfile.Api.Queries.Companies.GetAllCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            this.CreateMap<Category, CategoryDto>();
        }
    }
}
