using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Manufacturer
{
    public sealed class GetManufacturerQueryHandler : IQueryHandler<GetManufacturerQuery, ManufacturerDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetManufacturerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ManufacturerDto> HandleAsync(GetManufacturerQuery query)
        {
            var Manufacturer = await _unitOfWork.ManufacturersRepository.GetAsync(query.Id);

            if (Manufacturer == null)
            {
                throw new NullReferenceException("Manufacturer does not exist!");
            }

            return _mapper.Map<ManufacturerDto>(Manufacturer);
        }
    }
}
