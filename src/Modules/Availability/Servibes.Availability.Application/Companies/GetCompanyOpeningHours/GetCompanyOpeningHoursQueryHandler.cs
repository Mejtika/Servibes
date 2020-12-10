using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Application.Companies.GetCompanyOpeningHours
{
    public class GetCompanyOpeningHoursQueryHandler : IRequestHandler<GetCompanyOpeningHoursQuery, List<HoursRange>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyOpeningHoursQueryHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<HoursRange>> Handle(GetCompanyOpeningHoursQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            return company.GetOpeningHours();
        }
    }
}