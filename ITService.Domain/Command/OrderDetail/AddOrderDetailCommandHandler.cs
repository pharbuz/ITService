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

            var OrderDetail = _mapper.Map<Entities.OrderDetail>(command);
            OrderDetail.Id = Guid.NewGuid();
            OrderDetail.CreDate = DateTime.Now;
            OrderDetail.ModDate = DateTime.Now;
            await _unitOfWork.OrderDetailsRepository.AddAsync(OrderDetail);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
