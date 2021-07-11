using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Domain.Entities
{
    public partial class Service
    {
        public Service()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public Guid? CategryId { get; set; }
        public string Description { get; set; }

        public virtual Category Categry { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
