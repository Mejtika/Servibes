using System;
using System.Threading.Tasks;

namespace Servibes.Appointments.Core.Reservees
{
    public interface ICompanyRepository
    {
        Task<bool> ExistsByWalkInIdAsync(Guid companyId, Guid walkInId);
        Task<bool> ExistsByOwnerIdAsync(Guid companyId, Guid ownerId);
        Task AddAsync(Company company);
    }
}

