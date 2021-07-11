﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.User
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(LoginCommand command)
        {
            var validationResult = await new LoginCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var token = await _unitOfWork.UsersRepository.LoginAsync(command.Login, command.Password, command.RememberMe);
            if (string.IsNullOrEmpty(token))
            {
                return Result.Fail("Invalid username or password.");
            }

            return new Result(true, token, Enumerable.Empty<Result.Error>());
        }
    }
}
