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

            RuleFor(x => x.Duration).Must(x => x % 15 == 0)
                .WithMessage("Duration must be in 15 minutes intervals.");
        }
    }
}
