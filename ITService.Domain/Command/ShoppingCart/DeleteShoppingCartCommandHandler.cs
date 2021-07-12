using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class DeleteShoppingCartCommandHandler : ICommandHandler<DeleteShoppingCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteShoppingCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteShoppingCartCommand command)
        {
            var shoppingCart = await _unitOfWork.ShoppingCartsRepository.GetAsync(command.Id);
            if (shoppingCart == null)
            {
                return Result.Fail("ShoppingCart does not exist.");
            }

            await _unitOfWork.ShoppingCartsRepository.DeleteAsync(shoppingCart);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
