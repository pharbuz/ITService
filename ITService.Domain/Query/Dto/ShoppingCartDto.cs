using System;

namespace ITService.Domain.Query.Dto
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public ProductDto Product { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
