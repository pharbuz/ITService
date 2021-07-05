using System;

namespace ITService.Domain.Command.Service
{
    public sealed class DeleteServiceCommand : ICommand
    {
        public DeleteServiceCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
