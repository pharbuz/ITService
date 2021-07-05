using FluentValidation;

namespace ITService.Domain.Command.Employee
{
    internal class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
    {
        public AddEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(64);
            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.EmployeeComment)
                .MaximumLength(2048);
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
