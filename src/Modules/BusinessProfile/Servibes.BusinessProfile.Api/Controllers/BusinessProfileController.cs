using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Model.Enumerations;
using Servibes.BusinessProfile.Api.Model.ValueObjects;
using Servibes.BusinessProfile.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessProfileController : ControllerBase
    {
        private readonly BusinessProfileContext context;

        public BusinessProfileController(BusinessProfileContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public void CreateProfile([FromBody]CreateProfileDto profileDto)
        {
            //foreach Employee of Employees -> EmployeeAddedEvent -> Id, WorkingHours(CompanyOpeningHours) | Employee

            List<Employee> companyEmployees = new List<Employee>();
            profileDto.Employees.ForEach(e =>
            {
                companyEmployees.Add(new Employee() { FirstName = e.FirstName, LastName = e.LastName, WorkingHours = profileDto.OpeningHours });
            });

            List<Service> companyServices = new List<Service>();
            profileDto.Services.ForEach(s =>
            {
                List<Employee> serviceEmployees = new List<Employee>();
                s.Employees.Where(s => s.IsActive).ToList().ForEach(e =>
                {
                    serviceEmployees.Add(companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName));
                });

                companyServices.Add(new Service() 
                { 
                    ServiceName = s.ServiceName, 
                    Description = s.Description, 
                    Duration = s.Duration, 
                    Price = s.Price, 
                    Employees = serviceEmployees
                });
            });

            Company company = new Company()
            {
                CompanyName = profileDto.CompanyName,
                PhoneNumber = PhoneNumber.Create(profileDto.CompanyPhoneNumber), 
                Address = Address.Create(profileDto.Address.City,
                    profileDto.Address.ZipCode,
                    profileDto.Address.Street,
                    profileDto.Address.StreetNumber,
                    profileDto.Address.FlatNumber),
                Category = profileDto.Category,
                CoverPhoto = profileDto.CoverPhoto,
                OpeningHours = profileDto.OpeningHours,
                //Employees = companyEmployees,
                //Services = companyServices
            };

            context.Companies.Add(company);
            context.Employees.AddRange(companyEmployees);
            context.Services.AddRange(companyServices);
            context.SaveChanges();
        }
    }
}
