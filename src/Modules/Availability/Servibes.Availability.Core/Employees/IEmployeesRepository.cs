using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servibes.Availability.Core.Employees
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAllByCompanyIdAsync(Guid companyId);
    }
}