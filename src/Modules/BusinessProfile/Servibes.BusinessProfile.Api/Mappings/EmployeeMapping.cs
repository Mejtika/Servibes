using AutoMapper;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Queries.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Mappings
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            this.CreateMap<Employee, CompanyEmployeeDto>();
        }
    }
}
