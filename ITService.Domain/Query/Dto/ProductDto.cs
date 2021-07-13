using System;

namespace ITService.Domain.Query.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public Guid CategoryId { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public Guid ManufacturerId { get; set; }
    }
}
