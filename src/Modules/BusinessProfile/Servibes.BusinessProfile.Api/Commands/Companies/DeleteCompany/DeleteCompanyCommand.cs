using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Companies.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
    }
}
