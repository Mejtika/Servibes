using System;
using FluentValidation;

namespace Servibes.Availability.Application.Employees.MakeTimeReservation
{
    public class MakeTimeReservationCommandValidator : AbstractValidator<MakeTimeReservationCommand>
    {
        public MakeTimeReservationCommandValidator()
        {

            RuleFor(x => x.CompanyId).NotNull().NotEmpty()
                .WithMessage("CompanyId must not be empty.");

            RuleFor(x => x.EmployeeId).NotNull().NotEmpty()
                .WithMessage("EmployeeId must not be empty.");

            RuleFor(x => x.Start).Must(x => x > DateTime.Now)
                .WithMessage("Start date must be in the feature.");

            RuleFor(x => x.End).Must(x => x > DateTime.Now)
                .WithMessage("End date must be in the feature.");

            RuleFor(m => m.End)
                .GreaterThan(m => m.Start)
                .WithMessage("End date must be greater than start date.");
        }
    }
}
