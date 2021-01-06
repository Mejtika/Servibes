using System;
using FluentValidation;

namespace Servibes.Availability.Application.Employees.CancelTimeOff
{
    public class CancelTimeOffCommandValidator : AbstractValidator<CancelTimeOffCommand>
    {
        public CancelTimeOffCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty()
                .WithMessage("CompanyId must not be empty.");

            RuleFor(x => x.EmployeeId).NotNull().NotEmpty()
                .WithMessage("EmployeeId must not be empty.");

            RuleFor(x => x.Start).Must(x => x > DateTime.Now)
                .WithMessage("Start date must be in the feature.");
        }
    }
}
