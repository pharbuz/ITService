using System;
using FluentValidation;

namespace ITService.Domain.Command.Category
{
    internal class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
