using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ITService.Domain.Command.User
{
    public class EditUserPasswordCommandValidator : AbstractValidator<EditUserPasswordCommand>
    {
        private readonly Entities.User _user;
        private readonly IPasswordHasher<Entities.User> _hasher;

        public EditUserPasswordCommandValidator(Entities.User user, IPasswordHasher<Entities.User> hasher)
        {
            _user = user;
            _hasher = hasher;

            RuleFor(x => x.OldPassword)
                .NotNull()
                .NotEmpty()
                .Custom((x, y) =>
                {
                    if (x != null)
                    {
                        string passwordInDatabase = _user.Password;
                        string providedPassword = y.InstanceToValidate.OldPassword;
                        var result = _hasher.VerifyHashedPassword(_user, passwordInDatabase, providedPassword);
                        if (result != PasswordVerificationResult.Success)
                        {
                            y.AddFailure("You have entered a wrong password.");
                        }
                    }
                });
            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty()
                .Custom((a, b) =>
                {
                    if (a != null)
                    {
                        if (!a.Equals(b.InstanceToValidate.ConfirmNewPassword))
                        {
                            b.AddFailure("Passwords must be the same!");
                        }
                    }
                });
            RuleFor(y => y.ConfirmNewPassword)
                .NotNull()
                .NotEmpty();
        }
    }
}
