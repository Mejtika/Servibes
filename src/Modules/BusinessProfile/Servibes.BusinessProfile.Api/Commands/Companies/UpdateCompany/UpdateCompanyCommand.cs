using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Companies.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
        public UpdateCompanyDto UpdateCompanyDto { get; set; }
    }
}
