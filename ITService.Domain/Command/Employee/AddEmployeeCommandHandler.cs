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

            var Employee = _mapper.Map<Entities.Employee>(command);
            Employee.Id = Guid.NewGuid();

            Employee.CreDate = DateTime.Now;
            Employee.ModDate = DateTime.Now;

            await _unitOfWork.EmployeesRepository.AddAsync(Employee);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
