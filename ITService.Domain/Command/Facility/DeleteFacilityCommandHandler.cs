using System.Threading.Tasks;
using ITService.Domain.Repositories;

namespace ITService.Domain.Command.Facility
{
    public sealed class DeleteFacilityCommandHandler : ICommandHandler<DeleteFacilityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFacilityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(DeleteFacilityCommand command)
        {
            var Facility = await _unitOfWork.FacilitiesRepository.GetAsync(command.Id);
            if (Facility == null)
            {
                return Result.Fail("Facility does not exist.");
            }

            await _unitOfWork.FacilitiesRepository.DeleteAsync(Facility);
            await _unitOfWork.CommitAsync();

            return Result.Ok();
        }
    }
}
