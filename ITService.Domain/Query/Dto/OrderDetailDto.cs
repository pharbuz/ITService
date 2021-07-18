using System;

namespace ITService.Domain.Query.Dto
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
