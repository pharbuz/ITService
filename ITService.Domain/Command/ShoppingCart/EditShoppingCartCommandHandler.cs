using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class EditShoppingCartCommandHandler : ICommandHandler<EditShoppingCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditShoppingCartCommand command)
        {
            var validationResult = await new EditShoppingCartCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var shoppingCart = await _unitOfWork.ShoppingCartsRepository.GetAsync(command.Id);
            if (shoppingCart == null)
            {
                return Result.Fail("ShoppingCart does not exist.");
            }

            _mapper.Map(command, shoppingCart);

            await _unitOfWork.ShoppingCartsRepository.UpdateAsync(shoppingCart);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
