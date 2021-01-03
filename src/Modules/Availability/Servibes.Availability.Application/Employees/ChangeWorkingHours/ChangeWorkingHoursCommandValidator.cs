using FluentValidation;

namespace Servibes.Availability.Application.Employees.ChangeWorkingHours
{
    public class ChangeWorkingHoursCommandValidator : AbstractValidator<ChangeWorkingHoursCommand>
    {
        public ChangeWorkingHoursCommandValidator()
        {
            RuleFor(x => x.WorkingHours).NotNull().NotEmpty()
                .WithMessage("Working hours must not be empty.");
        }
    }
}
