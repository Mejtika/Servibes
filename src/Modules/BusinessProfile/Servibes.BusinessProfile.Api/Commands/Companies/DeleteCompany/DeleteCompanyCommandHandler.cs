using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Companies.DeleteCompany
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly BusinessProfileContext _context;

        public DeleteCompanyCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .SingleOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);
            if (company == null)
            {
                throw new AppException($"Company with id {request.CompanyId} not found.");
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
