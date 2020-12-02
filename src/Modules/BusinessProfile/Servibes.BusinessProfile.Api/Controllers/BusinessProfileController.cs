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
    [Route("api/company")]
    public class BusinessProfileController : ControllerBase
    {
        private readonly BusinessProfileContext context;

        public BusinessProfileController(BusinessProfileContext context)
        {
            this.context = context;
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompanyById(Guid companyId)
        {
            //TODO map to DTO
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            return Ok(company);
        }

        [HttpGet("{companyId}/openingHours")]
        public IActionResult GetOpeningHoursByCompanyId(Guid companyId)
        {
            //TODO map to DTO
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            if (company.OpeningHours.Count() == 0) //Should never happend!
                throw new ArgumentException($"Company with id {companyId} doesnt have any OpeningHours defined.");

            return Ok(company.OpeningHours);
        }

        [HttpPost]
        public IActionResult CreateProfile([FromBody]CreateProfileDto profileDto)
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
                List<Performer> servicePerformers = new List<Performer>();
                s.Performers.Where(p => p.IsActive).ToList().ForEach(sp =>
                {
                    servicePerformers.Add(new Performer()
                    {
                        PerformerId = companyEmployees.Where(ce => ce.FirstName == sp.FirstName && ce.LastName == sp.LastName).FirstOrDefault().EmployeeId
                    });
                });

                companyServices.Add(new Service()
                {
                    ServiceId = Guid.NewGuid(),
                    CompanyId = companyId,
                    ServiceName = s.ServiceName,
                    Description = s.Description,
                    Duration = s.Duration,
                    Price = s.Price,
                    Performers = servicePerformers
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

            return CreatedAtAction(nameof(GetCompanyById), new { companyId } );
        }

        [HttpPut("{companyId}/companyInfo")]
        public IActionResult UpdateCompanyInfo([FromBody]UpdateCompanyInfoDto companyInfoDto, Guid companyId)
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

            return NoContent();
        }

        [HttpPut("{companyId}/openingHours")]
        public IActionResult UpdateOpenHours([FromBody]List<OpenHoursDto> openingHours, Guid companyId)
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

            return NoContent();
        }
    }
}
