using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.User
{
    public sealed class EditUserDetailsCommandHandler : ICommandHandler<EditUserDetailsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditUserDetailsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditUserDetailsCommand command)
        {
            var validationResult = await new EditUserDetailsCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var user = await _unitOfWork.UsersRepository.GetAsync(command.Id);
            if (user == null)
            {
                return Result.Fail("User does not exist.");
            }

            user.PhoneNumber = command.PhoneNumber;
            user.Street = command.Street;
            user.City = command.City;
            user.PostalCode = command.PostalCode;
            user.LockoutEnd = command.LockoutEnd;

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
