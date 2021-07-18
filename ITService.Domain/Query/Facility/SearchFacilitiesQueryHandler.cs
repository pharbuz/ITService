using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using ITService.Domain.Query.Dto;
using ITService.Domain.Query.Dto.Pagination.PageResults;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Facility
{
    public sealed class SearchFacilitiesQueryHandler : IQueryHandler<SearchFacilitiesQuery, FacilityPageResult<FacilityDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchFacilitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FacilityPageResult<FacilityDto>> HandleAsync(SearchFacilitiesQuery query)
        {
            var validationResult = await new SearchFacilitiesQueryValidator().ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.FacilitiesRepository.SearchAsync(
                query.SearchPhrase,
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.SortDirection
            );

            return new FacilityPageResult<FacilityDto>(_mapper.Map<List<FacilityDto>>(result.Items), result.TotalItemsCount, query.PageSize, query.PageNumber);
        }
    }
}
