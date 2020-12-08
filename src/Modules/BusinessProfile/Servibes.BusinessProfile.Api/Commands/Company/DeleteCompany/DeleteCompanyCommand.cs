using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Commands.Company.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest
    {
        public Guid CompanyId { get; set; }
    }
}
