using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.Availability.Application.Companies.ChangeOpeningHours
{
    public class ChangeOpeningHoursCommandValidator : AbstractValidator<ChangeOpeningHoursCommand>
    {
        public ChangeOpeningHoursCommandValidator()
        {
            RuleFor(x => x.OpeningHours).NotNull().NotEmpty()
                .WithMessage("Opening hours must not be empty.");
        }
    }
}
