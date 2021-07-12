using FluentValidation;

namespace ITService.Domain.Command.Category
{
    internal class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
