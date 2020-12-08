using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Companies.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly BusinessProfileContext _context;

        public DeleteCompanyCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = _context.Companies.SingleOrDefault(c => c.CompanyId == request.CompanyId);

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt exist.");

            var companyEmployees = _context.Employees.Where(e => e.CompanyId == request.CompanyId).ToList();
            var companyServices = _context.Services.Where(s => s.CompanyId == request.CompanyId).ToList();

            _context.Companies.Remove(company);
            _context.Employees.RemoveRange(companyEmployees);
            _context.Services.RemoveRange(companyServices);

            _context.SaveChanges();

            return Unit.Task;
        }
    }
}
