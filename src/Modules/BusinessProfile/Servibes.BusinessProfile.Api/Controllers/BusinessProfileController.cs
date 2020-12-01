using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Dto;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Model.ValueObjects;
using Servibes.BusinessProfile.Api.Models;
using System;
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

        [HttpGet("/company")]
        public ActionResult<Company> GetCompanyById(Guid companyId)
        {
            //TODO: Map the Company to CompanyDto
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            return Ok(company);
        }

        [HttpGet("/service")]
        public ActionResult<Service> GetServiceById(Guid serviceId)
        {
            //TODO: Map the Service to ServiceDto
            var service = context.Services.Where(s => s.ServiceId == serviceId).FirstOrDefault();

            if (service == null)
                throw new ArgumentException($"Service with id {serviceId} doesnt exist.");

            return Ok(service);
        }

        [HttpGet]
        public ActionResult<List<OpeningHours>> GetOpeningHoursByCompanyId(Guid companyId)
        {
            //TODO: Map the OpeningHours to OpeningHoursDto
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            if (company.OpeningHours.Count() == 0) //Should never happend!
                throw new ArgumentException($"Company with id {companyId} doesnt have any OpeningHours defined.");

            return Ok(company.OpeningHours);
        }

        [HttpPost("/register")]
        public ActionResult<Company> CreateProfile([FromBody]CreateProfileDto profileDto)
        {
            //foreach Employee of Employees -> EmployeeAddedEvent -> Id, WorkingHours(CompanyOpeningHours) | Employee

            var companyId = Guid.NewGuid();

            List<Employee> companyEmployees = new List<Employee>();
            profileDto.Employees.ForEach(e =>
            {
                companyEmployees.Add(new Employee() 
                { 
                    EmployeeId = Guid.NewGuid(), 
                    FirstName = e.FirstName, 
                    LastName = e.LastName, 
                    WorkingHours = profileDto.OpeningHours.Select(oh => new WorkingHours()
                    {
                        DayOfWeek = oh.DayOfWeek,
                        From = TimeSpan.Parse(oh.OpenHour),
                        To = TimeSpan.Parse(oh.CloseHour)
                    }).ToList(), 
                    CompanyId = companyId 
                });
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
                    ServiceId = Guid.NewGuid(),
                    CompanyId = companyId,
                    ServiceName = s.ServiceName, 
                    Description = s.Description, 
                    Duration = s.Duration, 
                    Price = s.Price, 
                    Employees = serviceEmployees
                });
            });

            Company company = new Company()
            {
                CompanyId = companyId,
                CompanyName = profileDto.CompanyName,
                PhoneNumber = PhoneNumber.Create(profileDto.PhoneNumber),
                Address = Address.Create(profileDto.Address.City,
                    profileDto.Address.ZipCode,
                    profileDto.Address.Street,
                    profileDto.Address.StreetNumber,
                    profileDto.Address.FlatNumber),
                Category = profileDto.Category,
                CoverPhoto = profileDto.CoverPhoto,
                OpeningHours = profileDto.OpeningHours.Select(oh => new OpeningHours()
                {
                    DayOfWeek = oh.DayOfWeek,
                    IsOpen = oh.IsOpen,
                    From = TimeSpan.Parse(oh.OpenHour),
                    To = TimeSpan.Parse(oh.CloseHour)
                }).ToList()
            };

            context.Companies.Add(company);
            context.Employees.AddRange(companyEmployees);
            context.Services.AddRange(companyServices);
            context.SaveChanges();

            return CreatedAtAction("GetCompanyById", new { companyId } );
        }

        [HttpPost("/update/info")]
        public ActionResult<Company> UpdateCompanyInfo([FromBody]UpdateCompanyInfoDto companyInfoDto, Guid companyId)
        {
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            company.CompanyName = companyInfoDto.CompanyName;
            company.PhoneNumber = PhoneNumber.Create(companyInfoDto.PhoneNumber);
            company.Category = companyInfoDto.Category;
            company.CoverPhoto = companyInfoDto.CoverPhoto;
            company.Address = Address.Create(companyInfoDto.Address.City,
                companyInfoDto.Address.ZipCode,
                companyInfoDto.Address.Street,
                companyInfoDto.Address.StreetNumber,
                companyInfoDto.Address.FlatNumber);

            context.SaveChanges();

            return CreatedAtAction("GetCompanyById", new { companyId });
        }

        [HttpPost("/update/openhours")]
        public ActionResult<OpeningHours> UpdateOpenHours([FromBody]List<OpenHoursDto> openingHours, Guid companyId)
        {
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            company.OpeningHours = openingHours.Select(oh => new OpeningHours()
            {
                DayOfWeek = oh.DayOfWeek,
                IsOpen = oh.IsOpen,
                From = TimeSpan.Parse(oh.OpenHour),
                To = TimeSpan.Parse(oh.CloseHour)
            }).ToList();

            return CreatedAtAction("GetOpeningHoursByCompanyId", new { companyId });
        }

        [HttpPost("/update/service")]
        public ActionResult<Service> UpdateService([FromBody] ServiceDto serviceDto, Guid serviceId)
        {
            var service = context.Services.Where(s => s.ServiceId == serviceId).FirstOrDefault();

            if (service == null)
                throw new ArgumentException($"Service with id {serviceId} doesnt exist.");

            var companyEmployees = context.Employees.Where(e => e.CompanyId == service.CompanyId).ToList();

            if (companyEmployees.Count == 0)
                throw new ArgumentException($"Company with service of id {serviceId} doesnt have any employees.");

            service.ServiceName = serviceDto.ServiceName;
            service.Price = service.Price;
            service.Duration = service.Duration;
            service.Description = service.Description;

            service.Employees.Clear();
            serviceDto.Employees.Where(s => s.IsActive).ToList().ForEach(e =>
            {
                service.Employees.Add(companyEmployees.FirstOrDefault(ce => ce.FirstName == e.FirstName && ce.LastName == e.LastName));
            });

            return CreatedAtAction("GetServiceById", new { serviceId });
        }
    }
}
