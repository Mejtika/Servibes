using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Company.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public UpdateCompanyDto UpdateCompanyDto { get; set; }
    }
}
