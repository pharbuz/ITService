using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Service
{
    public sealed class DeleteServiceCommandHandler : ICommandHandler<DeleteServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteServiceCommand command)
        {
            var Service = await _unitOfWork.ServicesRepository.GetAsync(command.Id);
            if (Service == null)
            {
                return Result.Fail("Service does not exist.");
            }

            await _unitOfWork.ServicesRepository.DeleteAsync(Service);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
