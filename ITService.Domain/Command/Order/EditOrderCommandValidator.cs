using System;
using FluentValidation;

namespace ITService.Domain.Command.Order
{
    internal class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
    {
        public EditOrderCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.OrderStatus)
                .MaximumLength(50);
            RuleFor(x => x.OrderDate)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Carrier)
                .MaximumLength(50);
            RuleFor(x => x.City)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.OrderTotal)
                .NotEmpty()
                .NotNull()
                .NotEqual(0f);
            RuleFor(x => x.PaymentDate)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.PaymentDueDate)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.PaymentStatus)
                .MaximumLength(50);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);
            RuleFor(x => x.ShippingDate)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Street)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x => x.TrackingNumber)
                .MaximumLength(100);
            RuleFor(x => x.TransactionId)
                .MaximumLength(100);
        }
    }
}
