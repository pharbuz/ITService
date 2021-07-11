﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITService.Domain.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

  
        public Guid Id { get; set; }
        public Guid UserId { get; set; }     
        public User User{ get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }

    }
}
