using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Commands.Company.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public UpdateCompanyDto UpdateCompanyDto { get; set; }
    }
}
