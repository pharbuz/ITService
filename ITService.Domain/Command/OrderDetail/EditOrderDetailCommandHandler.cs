using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class EditOrderDetailCommandHandler : ICommandHandler<EditOrderDetailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditOrderDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditOrderDetailCommand command)
        {
            var validationResult = await new EditOrderDetailCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var OrderDetail = await _unitOfWork.OrderDetailsRepository.GetAsync(command.Id);
            if (OrderDetail == null)
            {
                return Result.Fail("OrderDetail does not exist.");
            }

            _mapper.Map(command, OrderDetail);

            OrderDetail.ModDate = DateTime.Now;

            await _unitOfWork.OrderDetailsRepository.UpdateAsync(OrderDetail);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
