using System;
using ITService.Domain.Command;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class AddShoppingCartCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}