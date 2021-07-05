using FluentValidation;

namespace ITService.Domain.Command.Service
{
    internal class EditServiceCommandValidator : AbstractValidator<EditServiceCommand>
    {
        public EditServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
