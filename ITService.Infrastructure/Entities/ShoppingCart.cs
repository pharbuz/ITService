using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Infrastructure.Entities
{
    public partial class ShoppingCart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
