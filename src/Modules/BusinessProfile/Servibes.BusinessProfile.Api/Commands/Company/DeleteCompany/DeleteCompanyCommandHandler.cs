using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Company.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly BusinessProfileContext context;

        public DeleteCompanyCommandHandler(BusinessProfileContext context)
        {
            this.context = context;
        }

        public Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = context.Companies.Where(c => c.CompanyId == request.CompanyId).FirstOrDefault();

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt exist.");

            var companyEmployees = context.Employees.Where(e => e.CompanyId == request.CompanyId).ToList();
            var companyServices = context.Services.Where(s => s.CompanyId == request.CompanyId).ToList();

            context.Companies.Remove(company);
            context.Employees.RemoveRange(companyEmployees);
            context.Services.RemoveRange(companyServices);

            context.SaveChanges();

            return Unit.Task;
        }
    }
}
