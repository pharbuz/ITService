using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Employee
{
    public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteEmployeeCommand command)
        {
            var Employee = await _unitOfWork.EmployeesRepository.GetAsync(command.Id);
            if (Employee == null)
            {
                return Result.Fail("Employee does not exist.");
            }

            await _unitOfWork.EmployeesRepository.DeleteAsync(Employee);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
