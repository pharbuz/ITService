using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Product
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteProductCommand command)
        {
            var Product = await _unitOfWork.ProductsRepository.GetAsync(command.Id);
            if (Product == null)
            {
                return Result.Fail("Product does not exist.");
            }

            await _unitOfWork.ProductsRepository.DeleteAsync(Product);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
