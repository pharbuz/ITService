using System;
using FluentValidation;

namespace ITService.Domain.Command.User
{
    internal class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
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
            RuleFor(x => x.City)
                .NotEmpty();
            RuleFor(x => x.PostalCode)
                .NotEmpty();
            RuleFor(x => x.Street)
                .NotEmpty();
        }
    }
}
