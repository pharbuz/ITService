using FluentValidation;

namespace ITService.Domain.Command.OrderDetail
{
    internal class AddOrderDetailCommandValidator : AbstractValidator<AddOrderDetailCommand>
    {
        public AddOrderDetailCommandValidator()
        {
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
