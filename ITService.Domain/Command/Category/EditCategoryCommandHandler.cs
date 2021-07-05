using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Category
{
    public sealed class EditCategoryCommandHandler : ICommandHandler<EditCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditCategoryCommand command)
        {
            var validationResult = await new EditCategoryCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var Category = await _unitOfWork.CategorysRepository.GetAsync(command.Id);
            if (Category == null)
            {
                return Result.Fail("Category does not exist.");
            }

            _mapper.Map(command, Category);

            Category.ModDate = DateTime.Now;

            await _unitOfWork.CategorysRepository.UpdateAsync(Category);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
