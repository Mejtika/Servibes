using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Core.Companies
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company);

        Task<bool> ExistsAsync(Guid companyId);

        Task<Company> GetByIdAsync(Guid companyId);

        Task<Company> GetByIdWithNoTrackingAsync(Guid companyId);
    }
}
