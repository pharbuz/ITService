using System;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class DeleteOrderDetailCommand : ICommand
    {
        public DeleteOrderDetailCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
