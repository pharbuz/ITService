using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Employee
{
    public sealed class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddEmployeeCommand command)
        {
            var validationResult = await new AddEmployeeCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var employee = _mapper.Map<Entities.Employee>(command);
            employee.Id = Guid.NewGuid();

            await _unitOfWork.EmployeesRepository.AddAsync(employee);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
