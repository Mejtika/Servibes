using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servibes.BusinessProfile.Api.Services
{
    public class AuthorizationService
    {
        private readonly BusinessProfileContext _context;

        public AuthorizationService(BusinessProfileContext context)
        {
            _context = context;
        }

        public async Task<AuthorizationDto> CheckOwnership(Guid userId, Guid companyId)
        {
            var company = await _context.Companies.SingleOrDefaultAsync(x => x.CompanyId == companyId);
            return new AuthorizationDto
            {
                IsAuthorized = company.OwnerId == userId
            };
        }
    }
}

