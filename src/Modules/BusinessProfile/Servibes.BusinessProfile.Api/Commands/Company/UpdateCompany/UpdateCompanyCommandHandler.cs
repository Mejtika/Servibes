using MediatR;
using Servibes.BusinessProfile.Api.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Company.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly BusinessProfileContext _context;

        public UpdateCompanyCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = _context.Companies.FirstOrDefault(c => c.CompanyId == request.CompanyId);

            if (company == null)
                throw new ArgumentException($"Company with id {request.CompanyId} doesnt exist.");

            company.CompanyName = request.UpdateCompanyDto.CompanyName;
            company.PhoneNumber = PhoneNumber.Create(request.UpdateCompanyDto.PhoneNumber);
            company.Category = request.UpdateCompanyDto.Category;
            company.Description = request.UpdateCompanyDto.Description;
            company.CoverPhoto = request.UpdateCompanyDto.CoverPhoto;
            company.Address = Address.Create(
                request.UpdateCompanyDto.Address.City,
                request.UpdateCompanyDto.Address.ZipCode,
                request.UpdateCompanyDto.Address.Street,
                request.UpdateCompanyDto.Address.StreetNumber,
                request.UpdateCompanyDto.Address.FlatNumber);

            _context.SaveChanges();

            return Unit.Task;
        }
    }
}
