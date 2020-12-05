using MediatR;
using Servibes.BusinessProfile.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Queries.Company.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDto>>
    {
        public string Category { get; set; }
    }
}
