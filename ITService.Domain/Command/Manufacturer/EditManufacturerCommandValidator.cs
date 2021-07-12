using System;
using FluentValidation;

namespace ITService.Domain.Command.Manufacturer
{
    internal class EditManufacturerCommandValidator : AbstractValidator<EditManufacturerCommand>
    {
        public EditManufacturerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
