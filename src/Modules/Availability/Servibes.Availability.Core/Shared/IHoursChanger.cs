using System.Collections.Generic;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Core.Shared
{
    public interface IHoursChanger
    {
        public void Update(Company company, List<Employee> employees, List<HoursRange> newHours);
    }
}
