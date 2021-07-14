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

            _mapper.Map(command, user);

            await _unitOfWork.UsersRepository.UpdateAsync(user);

            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
