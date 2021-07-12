using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Service
{
    public sealed class AddServiceCommandHandler : ICommandHandler<AddServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddServiceCommand command)
        {
            var validationResult = await new AddServiceCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var service = _mapper.Map<Entities.Service>(command);
            service.Id = Guid.NewGuid();

            await _unitOfWork.ServicesRepository.AddAsync(service);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
