using System;
using FluentValidation;

namespace Servibes.Availability.Application.Employees.GiveTimeOff
{
    public class GiveTimeOffCommandValidator : AbstractValidator<GiveTimeOffCommand>
    {
        public GiveTimeOffCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty()
                .WithMessage("CompanyId must not be empty.");

            RuleFor(x => x.EmployeeId).NotNull().NotEmpty()
                .WithMessage("EmployeeId must not be empty.");

            RuleFor(x => x.Start).Must(x => x > DateTime.Now)
                .WithMessage("Start date must be in the feature.");

            RuleFor(x => x.End).Must(x => x > DateTime.Now)
                .WithMessage("Start date must be in the feature.");

            RuleFor(x => x.Start).Must(x => x > DateTime.Now)
                .WithMessage("Start date must be in the feature.");
        }
    }
}
