using System;
using FluentValidation;

namespace ITService.Domain.Command.User
{
    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256);
            RuleFor(x => x.RememberMe)
                .NotNull();
        }
    }
}
