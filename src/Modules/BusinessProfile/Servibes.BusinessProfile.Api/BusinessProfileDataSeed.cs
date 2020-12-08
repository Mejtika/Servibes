using Servibes.BusinessProfile.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servibes.BusinessProfile.Api
{
    public static class BusinessProfileDataSeed
    {
        private static Random _random = new Random();

        private static int NewRandomInt() => _random.Next(100);

        public static void SeedData(this BusinessProfileContext context)
        {
            if (context.Companies.Any())
                return;

            Seed(context);
        }

        private static Company CreateCompany()
        {
            var randomInt = NewRandomInt();

            Company company = new Company()
            {
                CompanyId = Guid.NewGuid(),
                CompanyName = "Company name " + randomInt,
                Description = "This is a test company " + randomInt + " description.",
                PhoneNumber = PhoneNumber.Create("123456789"),
                Category = "Barber",
                CoverPhoto = "C:/FakePath/photo" + randomInt + ".jpg",
                Address = Address.Create("Test City", "00-000", "Test street", NewRandomInt().ToString(), NewRandomInt().ToString())
            };

            return company;
        }

        private static Employee CreateEmployee(Guid companyId)
        {
            var randomInt = NewRandomInt();

            Employee employee = new Employee()
            {
                EmployeeId = Guid.NewGuid(),
                CompanyId = companyId,
                FirstName = "First name " + randomInt,
                LastName = "Last name " + randomInt
            };

            return employee;
        }

        private static Service CreateService(Guid companyId, IEnumerable<Employee> performers)
        {
            var randomInt = NewRandomInt();

            Service service = new Service()
            {
                ServiceId = Guid.NewGuid(),
                CompanyId = companyId,
                ServiceName = "Test service " + randomInt,
                Description = "Description of a test service " + randomInt,
                Duration = 10.0d,
                Price = 100.55d,
                Performers = performers.Select(p => new Performer() { PerformerId = p.EmployeeId }).ToList()
            };

            return service;
        }

        private static void Seed(BusinessProfileContext context)
        {
            Company company = CreateCompany();
            List<Employee> employees = new List<Employee>();
            List<Service> services = new List<Service>();

            // Creates 6 employees and 6 services (with 1..2..3..and so on, performers)
            for(int i = 0; i <= 5; i++)
            {
                employees.Add(CreateEmployee(company.CompanyId));
                services.Add(CreateService(company.CompanyId, employees));
            }

            context.Companies.Add(company);
            context.Employees.AddRange(employees);
            context.Services.AddRange(services);

            context.SaveChanges();
        }
    }
}
