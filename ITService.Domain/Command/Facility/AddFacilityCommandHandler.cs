using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Facility
{
    public sealed class AddFacilityCommandHandler : ICommandHandler<AddFacilityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddFacilityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(AddFacilityCommand command)
        {
            var validationResult = await new AddFacilityCommandValidator().ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var facility = _mapper.Map<Entities.Facility>(command);
            facility.Id = Guid.NewGuid();

            await _unitOfWork.FacilitiesRepository.AddAsync(facility);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
