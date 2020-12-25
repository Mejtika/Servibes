using System;
using FluentValidation;

namespace Servibes.Availability.Application.Employees.GetEmployeeAvailableHours
{
    public class GetEmployeeAvailableHoursQueryValidator : AbstractValidator<GetEmployeeAvailableHoursQuery>
    {
        public GetEmployeeAvailableHoursQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty()
                .WithMessage("CompanyId must not be empty.");

            RuleFor(x => x.EmployeeId).NotNull().NotEmpty()
                .WithMessage("EmployeeId must not be empty.");

            RuleFor(x => x.Date).Must(x => x.Date >= DateTime.Today.Date)
                .WithMessage("Date must be in the feature.");

            RuleFor(x => x.Duration).Must(x => x % 15 == 0)
                .WithMessage("Duration must be in 15 minutes intervals.");
        }
    }
}