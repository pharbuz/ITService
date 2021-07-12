using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Domain.Entities
{
    public class User
    {
     
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Street { get; set; }
        public string City { get; set; }  
        public string PostalCode { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
