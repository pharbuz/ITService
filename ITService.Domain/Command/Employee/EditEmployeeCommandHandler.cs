using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Employee
{
    public sealed class EditEmployeeCommandHandler : ICommandHandler<EditEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditEmployeeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditEmployeeCommand command)
        {
            var validationResult = await new EditEmployeeCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var Employee = await _unitOfWork.EmployeesRepository.GetAsync(command.Id);
            if (Employee == null)
            {
                return Result.Fail("Employee does not exist.");
            }

            _mapper.Map(command, Employee);

            Employee.ModDate = DateTime.Now;

            await _unitOfWork.EmployeesRepository.UpdateAsync(Employee);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
