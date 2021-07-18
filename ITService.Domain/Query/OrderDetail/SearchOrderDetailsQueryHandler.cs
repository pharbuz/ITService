using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.OrderDetail
{
    public class SearchOrderDetailsQueryHandler : IQueryHandler<SearchOrderDetailsQuery, OrderDetailPageResult<OrderDetailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchOrderDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDetailPageResult<OrderDetailDto>> HandleAsync(SearchOrderDetailsQuery query)
        {
            var validationResult = await new SearchOrderDetailsQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.OrderDetailsRepository.SearchAsync(
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection,
                query.OrderId
            );

            return new OrderDetailPageResult<OrderDetailDto>(_mapper.Map<List<OrderDetailDto>>(result.Items), result.TotalItemsCount, query.PageSize, query.PageNumber);
        }
    }
}
