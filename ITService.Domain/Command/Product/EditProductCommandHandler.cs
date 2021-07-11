using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Product
{
    public sealed class EditProductCommandHandler : ICommandHandler<EditProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditProductCommand command)
        {
            var validationResult = await new EditProductCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var product = await _unitOfWork.ProductsRepository.GetAsync(command.Id);
            if (product == null)
            {
                return Result.Fail("Product does not exist.");
            }

            _mapper.Map(command, product);

            await _unitOfWork.ProductsRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
