using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Infrastructure.Domain.Reservees
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppointmentsContext _appointmentsContext;

        public CompanyRepository(AppointmentsContext appointmentsContext)
        {
            _appointmentsContext = appointmentsContext;
        }

        public async Task<bool> ExistsByWalkInIdAsync(Guid companyId, Guid walkInId)
        {
            return await _appointmentsContext.Companies.AnyAsync(x => x.CompanyId == companyId && x.WalkInId == walkInId);
        }

        public async Task<bool> ExistsByOwnerIdAsync(Guid companyId, Guid ownerId)
        {
            return await _appointmentsContext.Companies.AnyAsync(x => x.CompanyId == companyId && x.OwnerId == ownerId);
        }

        public async Task AddAsync(Company company)
        {
            await _appointmentsContext.Companies.AddAsync(company);
        }
    }
}
