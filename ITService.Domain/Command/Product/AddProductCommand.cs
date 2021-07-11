using System;

namespace ITService.Domain.Command.Product
{
    public sealed class AddProductCommand : ICommand
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public Guid? CategoryId { get; set; }
        public string Description { get; set; }
    }
}