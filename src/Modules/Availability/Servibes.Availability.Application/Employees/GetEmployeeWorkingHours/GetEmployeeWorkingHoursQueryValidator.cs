using FluentValidation;

namespace Servibes.Availability.Application.Employees.GetEmployeeWorkingHours
{
    public class GetEmployeeWorkingHoursQueryValidator : AbstractValidator<GetEmployeeWorkingHoursQuery>
    {
        public GetEmployeeWorkingHoursQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty()
                .WithMessage("CompanyId must not be empty.");

            RuleFor(x => x.EmployeeId).NotNull().NotEmpty()
                .WithMessage("EmployeeId must not be empty.");
        }
    }
}
