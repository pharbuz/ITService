using System;
using System.Threading.Tasks;
using AutoMapper;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Facility
{
    public sealed class EditFacilityCommandHandler : ICommandHandler<EditFacilityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditFacilityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(EditFacilityCommand command)
        {
            var validationResult = await new EditFacilityCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult);
            }

            var facility = await _unitOfWork.FacilitiesRepository.GetAsync(command.Id);
            if (facility == null)
            {
                return Result.Fail("Facility does not exist.");
            }

            _mapper.Map(command, facility);

            await _unitOfWork.FacilitiesRepository.UpdateAsync(facility);
            await _unitOfWork.CommitAsync(); 

            return Result.Ok();
        }
    }
}
