using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public int? Amount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
