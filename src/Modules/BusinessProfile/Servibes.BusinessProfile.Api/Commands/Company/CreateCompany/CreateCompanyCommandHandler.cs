using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Servibes.BusinessProfile.Api.Model;
using System.Linq;
using Servibes.Shared.Communication.Brokers;

namespace Servibes.BusinessProfile.Api.Commands.Company.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly BusinessProfileContext context;
        private readonly IMessageBroker broker;

        public CreateCompanyCommandHandler(BusinessProfileContext context, IMessageBroker broker)
        {
            this.context = context;
            this.broker = broker;
        }

        public Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var companyId = Guid.NewGuid();

            List<Model.Employee> companyEmployees = new List<Model.Employee>();
            request.CompanyDto.Employees.ForEach(e =>
            {
                companyEmployees.Add(new Model.Employee()
                {
                    CompanyId = companyId,
                    EmployeeId = Guid.NewGuid(),
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                });
            });

            List<Model.Service> companyServices = new List<Model.Service>();
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

                companyServices.Add(new Model.Service()
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

            Model.Company company = new Model.Company()
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

            broker.PublishAsync(evencik);

            context.Companies.Add(company);
            context.Employees.AddRange(companyEmployees);
            context.Services.AddRange(companyServices);
            context.SaveChanges();

            return Task.FromResult(companyId);
        }
    }
}
