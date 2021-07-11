using System;
using FluentValidation;

namespace ITService.Domain.Command.Service
{
    internal class EditServiceCommandValidator : AbstractValidator<EditServiceCommand>
    {
        public EditServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .NotEqual(0m);
            RuleFor(x => x.Image)
                .NotEmpty();
            RuleFor(x => x.CategryId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
