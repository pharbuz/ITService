using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Order
{
    public sealed class AddOrderCommandHandler : ICommandHandler<AddOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddOrderCommand command)
        {
            var validationResult = await new AddOrderCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var order = _mapper.Map<Entities.Order>(command);
            order.Id = Guid.NewGuid();

            await _unitOfWork.OrdersRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            var res = new Result(true, order.Id.ToString(), Enumerable.Empty<Result.Error>());

            return res;
        }
    }
}
