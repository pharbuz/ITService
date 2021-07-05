using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Product
{
    public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> HandleAsync(GetProductQuery query)
        {
            var Product = await _unitOfWork.ProductsRepository.GetAsync(query.Id);

            if (Product == null)
            {
                throw new NullReferenceException("Product does not exist!");
            }

            return _mapper.Map<ProductDto>(Product);
        }
    }
}
