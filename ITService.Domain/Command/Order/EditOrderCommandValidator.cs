using System;
using FluentValidation;

namespace ITService.Domain.Command.Order
{
    internal class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Amount)
                .NotEmpty()
                .NotNull()
                .NotEqual(0m);
            RuleFor(x => x.OrderStatus)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.OrderDate)
                .NotEmpty()
                .NotNull();
        }
    }
}
