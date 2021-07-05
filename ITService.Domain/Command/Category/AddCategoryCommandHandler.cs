using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Category
{
    public sealed class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddCategoryCommand command)
        {
            var validationResult = await new AddCategoryCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var Category = _mapper.Map<Entities.Category>(command);
            Category.Id = Guid.NewGuid();

            Category.CreDate = DateTime.Now;
            Category.ModDate = DateTime.Now;

            await _unitOfWork.CategorysRepository.AddAsync(Category);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
