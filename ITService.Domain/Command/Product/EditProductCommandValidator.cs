using System;
using FluentValidation;

namespace ITService.Domain.Command.Product
{
    internal class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
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
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Description)
                .NotEmpty();
            RuleFor(x => x.ManufacturerId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
        }
    }
}
