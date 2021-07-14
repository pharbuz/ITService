using System;
using ITService.Domain.Command;
using ITService.Domain.Query.Dto;

namespace ITService.Domain.Command.ShoppingCart
{
    public sealed class AddShoppingCartCommand : ICommand
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}