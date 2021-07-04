using System;

namespace ITService.Domain.Command.Order
{
    public sealed class AddOrderCommand : ICommand
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }
        public Guid ContactId { get; set; }
    }
}