using MediatR;
using System;

namespace Servibes.BusinessProfile.Api.Commands.Company.CreateCompany
{
    public class CreateCompanyCommand : IRequest<Guid>
    {
        public CompanyDto CompanyDto { get; set; }
    }
}
