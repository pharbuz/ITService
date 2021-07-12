using System;

namespace ITService.Domain.Command.Facility
{
    public class DeleteFacilityCommand : ICommand
    {
        public DeleteFacilityCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
