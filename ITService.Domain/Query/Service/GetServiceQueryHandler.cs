using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Service
{
    public sealed class GetServiceQueryHandler : IQueryHandler<GetServiceQuery, ServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceDto> HandleAsync(GetServiceQuery query)
        {
            var service = await _unitOfWork.ServicesRepository.GetAsync(query.Id);

            if (service == null)
            {
                throw new NullReferenceException("Service does not exist!");
            }

            return _mapper.Map<ServiceDto>(service);
        }
    }
}
