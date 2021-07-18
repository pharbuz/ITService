using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.ShoppingCart
{
    public sealed class SearchShoppingCartsQueryHandler : IQueryHandler<SearchShoppingCartsQuery, ShoppingCartPageResult<ShoppingCartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchShoppingCartsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShoppingCartPageResult<ShoppingCartDto>> HandleAsync(SearchShoppingCartsQuery query)
        {
            var validationResult = await new SearchShoppingCartsQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.ShoppingCartsRepository.SearchAsync(
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection,
                query.UserId
            );

            return new ShoppingCartPageResult<ShoppingCartDto>(_mapper.Map<List<ShoppingCartDto>>(result.Items), result.TotalItemsCount, query.PageSize, query.PageNumber);
        }
    }
}
