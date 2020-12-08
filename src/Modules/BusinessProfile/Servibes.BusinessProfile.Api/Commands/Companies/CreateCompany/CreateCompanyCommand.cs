using System;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Companies.CreateCompany
{
    public class CreateCompanyCommand : IRequest<Guid>
    {
        public CompanyDto CompanyDto { get; set; }
    }
}
