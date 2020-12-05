using Microsoft.AspNetCore.Mvc;
using Servibes.BusinessProfile.Api.Dto;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Servibes.BusinessProfile.Api.Queries.Company.GetCompany;
using Servibes.BusinessProfile.Api.Queries.Company.GetAllCompanies;
using Servibes.Shared.Communication.Brokers;

namespace Servibes.BusinessProfile.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly BusinessProfileContext context;
        private readonly IMessageBroker _broker;
        private readonly IMediator _mediator;

        public CompanyController(BusinessProfileContext context, IMessageBroker broker, IMediator mediator)
        {
            this.context = context;
            _broker = broker;
            this._mediator = mediator;
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompanyById(Guid companyId)
        {
            var result = _mediator.Send(new GetCompanyQuery()
            {
                CompanyId = companyId
            });

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllCompanies(string category = "")
        {
            var result = _mediator.Send(new GetAllCompaniesQuery()
            {
                Category = category
            });

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProfile([FromBody]CreateProfileDto profileDto)
        {
            var companyId = Guid.NewGuid();

            List<Employee> companyEmployees = new List<Employee>();
            profileDto.Employees.ForEach(e =>
            {
                companyEmployees.Add(new Employee()
                {
                    CompanyId = companyId,
                    EmployeeId = Guid.NewGuid(),
                    FirstName = e.FirstName,
                    LastName = e.LastName,
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
                        PerformerId = companyEmployees.FirstOrDefault(ce => ce.FirstName == sp.FirstName && ce.LastName == sp.LastName).EmployeeId
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
                Description = profileDto.Description,
                CoverPhoto = profileDto.CoverPhoto,
            };

            var evencik = new RegistrationCompleted(
                profileDto.OpeningHours,
                companyEmployees.Select(x => x.EmployeeId).ToList(),
                companyId);

            _broker.PublishAsync(evencik);

            //context.Companies.Add(company);
            //context.Employees.AddRange(companyEmployees);
            //context.Services.AddRange(companyServices);
            //context.SaveChanges();

            return CreatedAtAction(nameof(GetCompanyById), new { companyId } );
        }

        [HttpPut("{companyId}")]
        public IActionResult UpdateCompanyInfo([FromBody]UpdateCompanyInfoDto companyInfoDto, Guid companyId)
        {
            var company = context.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {companyId} doesnt exist.");

            company.CompanyName = companyInfoDto.CompanyName;
            company.PhoneNumber = PhoneNumber.Create(companyInfoDto.PhoneNumber);
            company.Category = companyInfoDto.Category;
            company.Description = companyInfoDto.Description;
            company.CoverPhoto = companyInfoDto.CoverPhoto;
            company.Address = Address.Create(companyInfoDto.Address.City,
                companyInfoDto.Address.ZipCode,
                companyInfoDto.Address.Street,
                companyInfoDto.Address.StreetNumber,
                companyInfoDto.Address.FlatNumber);

            context.SaveChanges();

            return NoContent();
        }
    }
}
