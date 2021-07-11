using System;
using FluentValidation;

namespace ITService.Domain.Command.Order
{
    internal class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
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
