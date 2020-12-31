using System.Collections.Generic;
using System.Text;
using MediatR;
using static System.String;

namespace Servibes.BusinessProfile.Api.Queries.Companies.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<CompanyDto>>
    {
    }
}
