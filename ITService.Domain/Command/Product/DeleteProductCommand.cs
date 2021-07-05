using System;

namespace ITService.Domain.Command.Product
{
    public sealed class DeleteProductCommand : ICommand
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
