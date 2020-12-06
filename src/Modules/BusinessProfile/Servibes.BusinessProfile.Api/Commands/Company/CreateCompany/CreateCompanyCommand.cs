using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Company.CreateCompany
{
    public class CreateCompanyCommand : IRequest<Guid>
    {
        public CompanyDto CompanyDto { get; set; }
    }
}
