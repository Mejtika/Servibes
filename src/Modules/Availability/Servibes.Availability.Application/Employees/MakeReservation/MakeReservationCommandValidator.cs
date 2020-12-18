using System;
using FluentValidation;

namespace Servibes.Availability.Application.Employees.MakeReservation
{
    public class MakeReservationCommandValidator : AbstractValidator<MakeReservationCommand>
    {
        public MakeReservationCommandValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId must not be empty.");
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty().WithMessage("EmployeeId must not be empty.");
            RuleFor(x => x.ReserveeId).NotNull().NotEmpty().WithMessage("ReserveeId must not be empty.");
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId must not be empty.");
            RuleFor(x => x.Start).Must(x => x > DateTime.Now)
                .WithMessage("Start date must be in the feature.");
        }
    }
}
