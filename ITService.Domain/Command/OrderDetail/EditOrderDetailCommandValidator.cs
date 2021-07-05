using FluentValidation;

namespace ITService.Domain.Command.OrderDetail
{
    internal class EditOrderDetailCommandValidator : AbstractValidator<EditOrderDetailCommand>
    {
        public EditOrderDetailCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Content)
                .MaximumLength(2048);
            RuleFor(x => x.ContactId)
                .NotEmpty();
        }
    }
}
