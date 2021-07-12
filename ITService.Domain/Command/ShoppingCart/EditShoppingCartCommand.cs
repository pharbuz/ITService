using System;
using ITService.Domain.Command;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class EditShoppingCartCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
