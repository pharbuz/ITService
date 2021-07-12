using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Manufacturer
{
    public sealed class EditManufacturerCommandHandler : ICommandHandler<EditManufacturerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditManufacturerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditManufacturerCommand command)
        {
            var validationResult = await new EditManufacturerCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var manufacturer = await _unitOfWork.ManufacturersRepository.GetAsync(command.Id);
            if (manufacturer == null)
            {
                return Result.Fail("Manufacturer does not exist.");
            }

            _mapper.Map(command, manufacturer);

            await _unitOfWork.ManufacturersRepository.UpdateAsync(manufacturer);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
