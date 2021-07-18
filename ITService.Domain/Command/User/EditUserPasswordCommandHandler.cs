using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ITService.Domain.Command.User
{
    public class EditUserPasswordCommandHandler : ICommandHandler<EditUserPasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Entities.User> _hasher;

        public EditUserPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<Entities.User> hasher)
        {
            _hasher = hasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditUserPasswordCommand command)
        {
            var user = await _unitOfWork.UsersRepository.GetAsync(command.Id);
            if (user == null)
            {
                return Result.Fail("User does not exist.");
            }

            var validationResult = await new EditUserPasswordCommandValidator(user, _hasher).ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            user.Password = _hasher.HashPassword(user, command.NewPassword);

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
