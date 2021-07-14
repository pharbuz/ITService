using System;
using FluentValidation;

namespace ITService.Domain.Command.OrderDetail
{
    internal class AddOrderDetailCommandValidator : AbstractValidator<AddOrderDetailCommand>
    {
        public AddOrderDetailCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .NotEqual(0m);
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .NotEqual(0);
        }
    }
}
