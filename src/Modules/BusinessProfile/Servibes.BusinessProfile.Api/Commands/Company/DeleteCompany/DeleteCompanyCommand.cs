using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.BusinessProfile.Api.Commands.Company.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
    }
}
