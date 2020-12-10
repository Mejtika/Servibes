using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Infrastructure.Domain.Companies
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AvailabilityContext _availabilityContext;

        public CompanyRepository(AvailabilityContext availabilityContext)
        {
            _availabilityContext = availabilityContext;
        }

        public async Task AddAsync(Company company)
        {
            await _availabilityContext.AddAsync(company);
        }

        public async Task<bool> ExistsAsync(Guid companyId)
        {
            return await _availabilityContext.Companies.AsNoTracking().AnyAsync(x => x.CompanyId == companyId);
        }

        public async Task<Company> GetByIdAsync(Guid companyId)
        {
            return await _availabilityContext.Companies.AsNoTracking().SingleOrDefaultAsync(x => x.CompanyId == companyId);
        }
    }
}
