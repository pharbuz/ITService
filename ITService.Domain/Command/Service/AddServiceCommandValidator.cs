using System;
using FluentValidation;

namespace ITService.Domain.Command.Service
{
    internal class AddServiceCommandValidator : AbstractValidator<AddServiceCommand>
    {
        public AddServiceCommandValidator()
        {
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
                .GreaterThan(0)
                .NotEqual(0f);
        }
    }
}
