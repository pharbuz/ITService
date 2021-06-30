using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Domain.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            Services = new HashSet<Service>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
