using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Service
{
    public sealed class AddServiceCommandHandler : ICommandHandler<AddServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddServiceCommand command)
        {
            var validationResult = await new AddServiceCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var role = _mapper.Map<Entities.Role>(command);
            await _unitOfWork.RolesRepository.AddAsync(role);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
