using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Order
{
    public sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteOrderCommand command)
        {
            var order = await _unitOfWork.OrdersRepository.GetAsync(command.Id);
            if (order == null)
            {
                return Result.Fail("Order does not exist.");
            }

            await _unitOfWork.OrdersRepository.DeleteAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
