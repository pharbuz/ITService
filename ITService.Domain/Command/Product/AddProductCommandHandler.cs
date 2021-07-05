using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Product
{
    public sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddProductCommand command)
        {
            var validationResult = await new AddProductCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var Product = _mapper.Map<Entities.Product>(command);
            Product.Id = Guid.NewGuid();
            Product.CreDate = DateTime.Now;
            Product.ModDate = DateTime.Now;
            await _unitOfWork.ProductsRepository.AddAsync(Product);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
