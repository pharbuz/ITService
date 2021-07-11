using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.OrderDetail
{
    public sealed class GetOrderDetailQueryHandler : IQueryHandler<GetOrderDetailQuery, OrderDetailDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDetailDto> HandleAsync(GetOrderDetailQuery query)
        {
            var orderDetail = await _unitOfWork.OrderDetailsRepository.GetAsync(query.Id);

            if (orderDetail == null)
            {
                throw new NullReferenceException("OrderDetail does not exist!");
            }

            return _mapper.Map<OrderDetailDto>(orderDetail);
        }
    }
}
