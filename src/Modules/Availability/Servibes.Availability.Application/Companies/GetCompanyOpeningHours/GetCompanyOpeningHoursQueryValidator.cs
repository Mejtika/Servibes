using FluentValidation;

namespace Servibes.Availability.Application.Companies.GetCompanyOpeningHours
{
    public class GetCompanyOpeningHoursQueryValidator : AbstractValidator<GetCompanyOpeningHoursQuery>
    {
        public GetCompanyOpeningHoursQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty()
                .WithMessage("CompanyId must not be empty.");
        }
    }
}