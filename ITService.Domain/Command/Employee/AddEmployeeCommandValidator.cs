using FluentValidation;

namespace ITService.Domain.Command.Employee
{
    internal class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
    {
        public AddEmployeeCommandValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256);
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Salary)
                .NotEmpty()
                .NotNull()
                .NotEqual(0m);
        }
    }
}
