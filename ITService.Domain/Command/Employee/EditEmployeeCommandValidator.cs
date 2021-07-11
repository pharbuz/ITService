using System;
using FluentValidation;

namespace ITService.Domain.Command.Employee
{
    internal class EditEmployeeCommandValidator : AbstractValidator<EditEmployeeCommand>
    {
        public EditEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
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
