using System.Collections.Generic;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Core.Shared
{
    public class HoursChanger : IHoursChanger
    {
        public void Update(Company company, List<Employee> employees, List<HoursRange> newHours)
        {
            company.ChangeOpeningHours(newHours);

            foreach (var employee in employees)
            {
                employee.AdjustWorkingHours(newHours);
            }
        }
    }
}