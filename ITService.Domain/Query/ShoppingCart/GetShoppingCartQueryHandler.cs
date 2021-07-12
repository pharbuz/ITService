using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.ShoppingCart
{
    public sealed class GetShoppingCartQueryHandler : IQueryHandler<GetShoppingCartQuery, ShoppingCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetShoppingCartQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ShoppingCartDto> HandleAsync(GetShoppingCartQuery query)
        {
            var ShoppingCart = await _unitOfWork.ShoppingCartsRepository.GetAsync(query.Id);

            if (ShoppingCart == null)
            {
                throw new NullReferenceException("ShoppingCart does not exist!");
            }

            return _mapper.Map<ShoppingCartDto>(ShoppingCart);
        }
    }
}
