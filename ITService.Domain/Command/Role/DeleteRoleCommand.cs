using System;

namespace ITService.Domain.Command.Role
{
    public sealed class DeleteRoleCommand : ICommand
    {
        public DeleteRoleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
