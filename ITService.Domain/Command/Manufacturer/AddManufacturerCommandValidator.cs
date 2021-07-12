using FluentValidation;

namespace ITService.Domain.Command.Manufacturer
{
    internal class AddManufacturerCommandValidator : AbstractValidator<AddManufacturerCommand>
    {
        public AddManufacturerCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
