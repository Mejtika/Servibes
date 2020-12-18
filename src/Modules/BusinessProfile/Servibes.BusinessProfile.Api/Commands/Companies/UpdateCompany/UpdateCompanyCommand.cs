using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Companies.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest
    {
        public Guid CompanyId { get; }
        public UpdateCompanyDto UpdateCompanyDto { get; }

        public UpdateCompanyCommand(Guid companyId, UpdateCompanyDto updateCompanyDto)
        {
            CompanyId = companyId;
            UpdateCompanyDto = updateCompanyDto;
        }
    }
}
