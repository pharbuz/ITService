using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Query.Dto;
using ITService.Domain.Repositories;

namespace ITService.Domain.Query.Employee
{
    public sealed class GetEmployeeQueryHandler : IQueryHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeDto> HandleAsync(GetEmployeeQuery query)
        {
            var employee = await _unitOfWork.EmployeesRepository.GetAsync(query.Id);

            if (employee == null)
            {
                throw new NullReferenceException("Employee does not exist!");
            }

            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
