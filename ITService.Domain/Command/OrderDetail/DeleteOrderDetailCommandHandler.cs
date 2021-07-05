using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class DeleteOrderDetailCommandHandler : ICommandHandler<DeleteOrderDetailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderDetailCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteOrderDetailCommand command)
        {
            var OrderDetail = await _unitOfWork.OrderDetailsRepository.GetAsync(command.Id);
            if (OrderDetail == null)
            {
                return Result.Fail("OrderDetail does not exist.");
            }

            await _unitOfWork.OrderDetailsRepository.DeleteAsync(OrderDetail);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
