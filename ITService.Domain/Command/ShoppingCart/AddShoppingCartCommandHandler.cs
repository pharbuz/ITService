using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class AddShoppingCartCommandHandler : ICommandHandler<AddShoppingCartCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddShoppingCartCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddShoppingCartCommand command)
        {
            var validationResult = await new AddShoppingCartCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var shoppingCart = new Entities.ShoppingCart()
            {
                Count = command.Count,
                ProductId = command.ProductId,
                UserId = command.UserId
            };
            shoppingCart.Id = Guid.NewGuid();

            await _unitOfWork.ShoppingCartsRepository.AddAsync(shoppingCart);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
