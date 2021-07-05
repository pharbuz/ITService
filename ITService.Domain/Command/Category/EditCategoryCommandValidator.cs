using FluentValidation;

namespace ITService.Domain.Command.Category
{
    internal class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(64);
            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.CategoryComment)
                .MaximumLength(2048);
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
