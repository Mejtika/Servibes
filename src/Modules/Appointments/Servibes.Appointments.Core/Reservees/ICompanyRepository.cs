using System;
using System.Threading.Tasks;

namespace Servibes.Appointments.Core.Reservees
{
    public interface ICompanyRepository
    {
        Task<bool> ExistsAsync(Guid companyId, Guid walkInId);
        Task AddAsync(Company company);
    }
}

