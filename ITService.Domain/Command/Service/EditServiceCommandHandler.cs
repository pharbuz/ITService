using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Service
{
    public sealed class EditServiceCommandHandler : ICommandHandler<EditServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditServiceCommand command)
        {
            var validationResult = await new EditServiceCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var service = await _unitOfWork.ServicesRepository.GetAsync(command.Id);
            if (service == null)
            {
                return Result.Fail("Service does not exist.");
            }

            _mapper.Map(command, service);

            await _unitOfWork.ServicesRepository.UpdateAsync(service);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
