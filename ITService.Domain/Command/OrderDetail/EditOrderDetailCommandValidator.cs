using System;
using System.Data;
using FluentValidation;

namespace ITService.Domain.Command.OrderDetail
{
    internal class EditOrderDetailCommandValidator : AbstractValidator<EditOrderDetailCommand>
    {
        public EditOrderDetailCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
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
                .NotEqual(0m);
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                .NotEqual(0);
        }
    }
}
