using System;
using System.Threading.Tasks;

namespace Servibes.Availability.Core.Employees
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);

        Task<Employee> GetByIdAsync(Guid employeeId);
    }
}
