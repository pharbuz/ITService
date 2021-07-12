using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Infrastructure.Entities
{
    public partial class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double EstimatedServicePrice { get; set; }
    }
}
