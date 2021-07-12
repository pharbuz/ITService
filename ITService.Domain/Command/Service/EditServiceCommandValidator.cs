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
            RuleFor(x => x.Image)
                .NotEmpty();
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.EstimatedServicePrice)
                .NotEmpty()
                .NotNull()
                .NotEqual(0f);
        }
    }
}
