using FluentValidation;
using static Application.CQRS.Users.Handlers.Update;

namespace Application.CQRS.Users.Validators;

public class UpdateUserValidator : AbstractValidator<Command>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Name)
         .NotEmpty()
         .MaximumLength(255);

        RuleFor(u => u.Email)
            .NotEmpty()
            .MaximumLength(70)
            .EmailAddress();
    }
}