using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Infrastructure.Domain.Employees
{
    class EmployeesRepository : IEmployeesRepository
    {
        private readonly AvailabilityContext _availabilityContext;

        public EmployeesRepository(AvailabilityContext availabilityContext)
        {
            _availabilityContext = availabilityContext;
        }

        public async Task<List<Employee>> GetAllByCompanyId(Guid companyId)
        {
            return await _availabilityContext.Employees.Where(x => x.CompanyId == companyId).ToListAsync();
        }
    }
}
