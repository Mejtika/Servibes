using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Infrastructure.Domain.Employees
{
    class EmployeeRepository : IEmployeeRepository
    {
        private readonly AvailabilityContext _availabilityContext;

        public EmployeeRepository(AvailabilityContext availabilityContext)
        {
            _availabilityContext = availabilityContext;
        }
        public async Task AddAsync(Employee employee)
        {
            await _availabilityContext.Employees.AddAsync(employee);
        }

        public async Task<Employee> GetByIdAsync(Guid employeeId)
        {
            return await _availabilityContext.Employees.SingleOrDefaultAsync(x => x.EmployeeId == employeeId);
        }
    }
}
