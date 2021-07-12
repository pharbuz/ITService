using System;
using FluentValidation;

namespace ITService.Domain.Command.ShoppingCart
{
    internal class EditShoppingCartCommandValidator : AbstractValidator<EditShoppingCartCommand>
    {
        public EditShoppingCartCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
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
                .NotEqual(0);
        }
    }
}
