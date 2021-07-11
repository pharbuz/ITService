using System;
using FluentValidation;

namespace ITService.Domain.Command.User
{
    internal class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256);
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
        }
    }
}
