using System;

namespace ITService.Domain.Command.Product
{
    public sealed class EditProductCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ManufacturerId { get; set; }
    }
}
