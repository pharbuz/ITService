using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Manufacturer
{
    public sealed class AddManufacturerCommandHandler : ICommandHandler<AddManufacturerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddManufacturerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddManufacturerCommand command)
        {
            var validationResult = await new AddManufacturerCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var manufacturer = _mapper.Map<Entities.Manufacturer>(command);
            manufacturer.Id = Guid.NewGuid();

            await _unitOfWork.ManufacturersRepository.AddAsync(manufacturer);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
