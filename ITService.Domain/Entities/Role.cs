using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Domain.Entities
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
