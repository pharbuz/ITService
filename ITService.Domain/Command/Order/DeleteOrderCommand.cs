using System;

namespace ITService.Domain.Command.Order
{
    public class DeleteOrderCommand : ICommand
    {
        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
