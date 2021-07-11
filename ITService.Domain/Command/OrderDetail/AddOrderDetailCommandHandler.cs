using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class AddOrderDetailCommandHandler : ICommandHandler<AddOrderDetailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddOrderDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddOrderDetailCommand command)
        {
            var validationResult = await new AddOrderDetailCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var orderDetail = _mapper.Map<Entities.OrderDetail>(command);
            orderDetail.Id = Guid.NewGuid();

            await _unitOfWork.OrderDetailsRepository.AddAsync(orderDetail);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
