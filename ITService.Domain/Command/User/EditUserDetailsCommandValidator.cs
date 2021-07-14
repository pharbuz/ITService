using System;
using FluentValidation;

namespace ITService.Domain.Command.User
{
    internal class EditUserDetailsCommandValidator : AbstractValidator<EditUserDetailsCommand>
    {
        public EditUserDetailsCommandValidator()
        {
            RuleFor(x => x.Id)
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
