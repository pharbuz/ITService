using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Domain.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? Salary { get; set; }
        public Guid? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
