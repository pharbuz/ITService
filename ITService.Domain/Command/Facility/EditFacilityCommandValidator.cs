using System;
using FluentValidation;

namespace ITService.Domain.Command.Facility
{
    internal class EditFacilityCommandValidator : AbstractValidator<EditFacilityCommand>
    {
        public EditFacilityCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(x => x.Name)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.StreetAdress)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(x => x.PostalCode)
                .MaximumLength(30)
                .NotEmpty();
            RuleFor(x => x.City)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(x => x.OpenedSaturday)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.OpenedWeek)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(x => x.MapUrl)
                .NotEmpty();
        }
    }
}
