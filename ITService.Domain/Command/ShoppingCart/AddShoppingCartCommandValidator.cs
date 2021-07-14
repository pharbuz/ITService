using System;
using FluentValidation;

namespace ITService.Domain.Command.ShoppingCart
{
    internal class AddShoppingCartCommandValidator : AbstractValidator<AddShoppingCartCommand>
    {
        public AddShoppingCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Count)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .NotEqual(0);
        }
    }
}
