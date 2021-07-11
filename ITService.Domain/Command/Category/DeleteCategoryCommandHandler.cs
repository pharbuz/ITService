using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Category
{
    public sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteCategoryCommand command)
        {
            var Category = await _unitOfWork.CategoriesRepository.GetAsync(command.Id);
            if (Category == null)
            {
                return Result.Fail("Category does not exist.");
            }

            await _unitOfWork.CategoriesRepository.DeleteAsync(Category);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
