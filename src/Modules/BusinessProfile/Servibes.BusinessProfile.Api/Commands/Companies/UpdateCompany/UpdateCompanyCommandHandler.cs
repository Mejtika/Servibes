using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models;
using Servibes.Shared.Exceptions;

namespace Servibes.BusinessProfile.Api.Commands.Companies.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly BusinessProfileContext _context;

        public UpdateCompanyCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(c => c.CompanyId == request.CompanyId, cancellationToken);

            if (company == null)
            {
                throw new AppException($"Company with id {request.CompanyId} not found.");
            }

            company.CompanyName = request.UpdateCompanyDto.CompanyName;
            company.PhoneNumber = PhoneNumber.Create(request.UpdateCompanyDto.PhoneNumber);
            company.Category = request.UpdateCompanyDto.Category;
            company.Description = request.UpdateCompanyDto.Description;
            company.Address = Address.Create(
                request.UpdateCompanyDto.Address.City,
                request.UpdateCompanyDto.Address.ZipCode,
                request.UpdateCompanyDto.Address.Street,
                request.UpdateCompanyDto.Address.StreetNumber,
                request.UpdateCompanyDto.Address.FlatNumber);

            //Update cover photo id only when incoming value is not empty
            if (!string.IsNullOrWhiteSpace(request.UpdateCompanyDto.CoverPhotoId))
                company.CoverPhotoId = Guid.Parse(request.UpdateCompanyDto.CoverPhotoId);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
