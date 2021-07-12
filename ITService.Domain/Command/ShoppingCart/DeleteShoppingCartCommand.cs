using System;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class DeleteShoppingCartCommand : ICommand
    {
        public DeleteShoppingCartCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
