using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Facility
{
    public sealed class GetFacilityQueryHandler : IQueryHandler<GetFacilityQuery, FacilityDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFacilityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FacilityDto> HandleAsync(GetFacilityQuery query)
        {
            var Facility = await _unitOfWork.FacilitiesRepository.GetAsync(query.Id);

            if (Facility == null)
            {
                throw new NullReferenceException("Facility does not exist!");
            }

            return _mapper.Map<FacilityDto>(Facility);
        }
    }
}
