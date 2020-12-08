using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.BusinessProfile.Api.Models;
using Servibes.Shared.Communication.Brokers;

namespace Servibes.BusinessProfile.Api.Commands.Companies.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly BusinessProfileContext _context;
        private readonly IMessageBroker _broker;

        public CreateCompanyCommandHandler(BusinessProfileContext context, IMessageBroker broker)
        {
            _context = context;
            _broker = broker;
        }

        public Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyId = Guid.NewGuid();

            List<Employee> companyEmployees = new List<Employee>();
            request.CompanyDto.Employees.ForEach(e =>
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
            request.CompanyDto.Services.ForEach(s =>
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
                CompanyName = request.CompanyDto.CompanyName,
                PhoneNumber = PhoneNumber.Create(request.CompanyDto.PhoneNumber),
                Address = Address.Create(request.CompanyDto.Address.City,
                    request.CompanyDto.Address.ZipCode,
                    request.CompanyDto.Address.Street,
                    request.CompanyDto.Address.StreetNumber,
                    request.CompanyDto.Address.FlatNumber),
                Category = request.CompanyDto.Category,
                Description = request.CompanyDto.Description,
                CoverPhoto = request.CompanyDto.CoverPhoto,
            };

            var evencik = new RegistrationCompleted(
                request.CompanyDto.OpeningHours,
                companyEmployees.Select(x => x.EmployeeId).ToList(),
                companyId);

            _broker.PublishAsync(evencik);

            _context.Companies.Add(company);
            _context.Employees.AddRange(companyEmployees);
            _context.Services.AddRange(companyServices);
            _context.SaveChanges();

            return Task.FromResult(companyId);
        }
    }
}
