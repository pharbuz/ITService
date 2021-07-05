using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Category
{
    public sealed class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> HandleAsync(GetCategoryQuery query)
        {
            var Category = await _unitOfWork.CategorysRepository.GetAsync(query.Id);

            if (Category == null)
            {
                throw new NullReferenceException("Category does not exist!");
            }

            return _mapper.Map<CategoryDto>(Category);
        }
    }
}
