using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Manufacturer
{
    public sealed class DeleteManufacturerCommandHandler : ICommandHandler<DeleteManufacturerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteManufacturerCommand command)
        {
            var manufacturer = await _unitOfWork.ManufacturersRepository.GetAsync(command.Id);
            if (manufacturer == null)
            {
                return Result.Fail("Manufacturer does not exist.");
            }

            await _unitOfWork.ManufacturersRepository.DeleteAsync(manufacturer);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
